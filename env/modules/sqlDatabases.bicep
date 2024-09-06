// Parameters
@description('Location for all resources.')
param resourceLocation string

@description('Location for replica resources.')
param replicaLocation string

@description('Global location name')
param globalLocation string

@description('Environment for all resources.')
@allowed([
  'test'
  'prod'
])
param environment string

@description('SQL logical server name.')
param sqlServerName string 

@description('SQL logical server replica name.')
param sqlServerReplicaName string

@description('SQL logical server administrator username.')
@secure()
param sqlServerAdminUser string

@description('SQL logical server administrator password.')
@secure()
param sqlServerAdminPassword string

@description('SQL logical server administrator login.')
param sqlServerAdminLogin string

@description('SQL logical server administrator SID.')
param sqlServerAdminSID string

@description('SQL logical server administrator principal type.')
@allowed([
  'Group'
  'User'
])
param sqlServerAdminType string

@description('SQL database name.')
param sqlDataBaseName string

@description('SQL database sku name.')
@allowed([
  'Basic'
  'S2'
  'P1'
])
param sqlDataBaseSkuName string

@description('Private endpoint name')
param privateEndpointSqlName string

@description('Private endpoint replica name')
param privateEndpointSqlReplicaName string

@description('Private sql dns zone name')
param privateDnsZoneSqlName string

@description('Existing virtual network.')
param virtualNetworkId string

@description('Existing virtual network.')
param virtualNetworkName string

@description('Existing subnet for sql databases.')
param dataSubnetId string

@description('Existing subnet for sql databases replica.')
param dataReplicaSubnetId string

// Variables

resource sqlServer 'Microsoft.Sql/servers@2021-11-01' = {
  name: sqlServerName
  location: resourceLocation
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    administratorLogin: sqlServerAdminUser
    administratorLoginPassword: sqlServerAdminPassword
    administrators: {
      administratorType: 'ActiveDirectory'
      azureADOnlyAuthentication: false
      login: sqlServerAdminLogin
      principalType: sqlServerAdminType
      sid: sqlServerAdminSID
      tenantId: subscription().tenantId
    }
    publicNetworkAccess: 'Enabled'
    minimalTlsVersion: '1.2'
  }
}

resource sqlServerReplica 'Microsoft.Sql/servers@2021-11-01' = if(environment=='prod') {
  name: sqlServerReplicaName
  location: replicaLocation
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    administratorLogin: sqlServerAdminUser
    administratorLoginPassword: sqlServerAdminPassword
    administrators: {
      administratorType: 'ActiveDirectory'
      azureADOnlyAuthentication: false
      login: sqlServerAdminLogin
      principalType: sqlServerAdminType
      sid: sqlServerAdminSID
      tenantId: subscription().tenantId
    }
    publicNetworkAccess: 'Enabled'
    minimalTlsVersion: '1.2'
  }
}

resource sqlDataBase 'Microsoft.Sql/servers/databases@2021-11-01' = {
  parent: sqlServer
  name: sqlDataBaseName
  location: resourceLocation
  sku: {
    name: sqlDataBaseSkuName
  }
}

resource sqlDataBaseReplica 'Microsoft.Sql/servers/databases@2021-11-01' = if(environment=='prod') {
  parent: sqlServerReplica
  name: sqlDataBaseName
  location: replicaLocation
  sku: {
    name: sqlDataBaseSkuName
  }
  properties:{
    createMode: 'Secondary'
    sourceDatabaseId: sqlDataBase.id
  }
}

resource privateEndpointSql 'Microsoft.Network/privateEndpoints@2023-09-01' = {
  name: privateEndpointSqlName
  location: resourceLocation
  properties: {
    subnet: {
      id: dataSubnetId
    }
    customNetworkInterfaceName: '${privateEndpointSqlName}-nic'
    privateLinkServiceConnections: [
      {
        name: privateEndpointSqlName
        properties: {
          privateLinkServiceId: sqlServer.id
          groupIds: [
            'sqlServer'
          ]
        }
      }
    ]
  }
}

resource privateEndpointSqlReplica 'Microsoft.Network/privateEndpoints@2023-09-01' = if(environment=='prod') {
  name: privateEndpointSqlReplicaName
  location: resourceLocation
  properties: {
    subnet: {
      id: dataReplicaSubnetId
    }
    customNetworkInterfaceName: '${privateEndpointSqlReplicaName}-nic'
    privateLinkServiceConnections: [
      {
        name: privateEndpointSqlReplicaName
        properties: {
          privateLinkServiceId: sqlServerReplica.id
          groupIds: [
            'sqlServer'
          ]
        }
      }
    ]
  }
}

resource privateDnsZoneSql 'Microsoft.Network/privateDnsZones@2020-06-01' =  {
  name: privateDnsZoneSqlName
  location: globalLocation
}

resource privateDnsZoneLinkSql  'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZoneSql
  name: '${privateDnsZoneSqlName}-${virtualNetworkName}'
  location: globalLocation
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkId
    }
  }
}

resource privateDnsZoneGroupSql 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-09-01' = {
  parent: privateEndpointSql
  name: 'default'  
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'default'
        properties: {
          privateDnsZoneId: privateDnsZoneSql.id
        }
      }
    ]
  }
}

resource privateDnsZoneGroupSqlReplica 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-09-01' = if(environment=='prod') {
  parent: privateEndpointSqlReplica
  name: 'default'  
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'default'
        properties: {
          privateDnsZoneId: privateDnsZoneSql.id
        }
      }
    ]
  }
}

// Outputs
output serverFQDN string = sqlServer.properties.fullyQualifiedDomainName
