// Basic parameters.
@description('Global location name')
param globalLocation string

@description('Replica location name.')
param replicaLocation string

@description('Resource location name.')
param resourceLocation string = resourceGroup().location

@description('Prefix name')
param prefix string

@description('Suffix name')
param suffix string = substring('${uniqueString(resourceGroup().id)}',0,6)

@description('Environment for all resources.')
@allowed([
  'test'
  'prod'
])
param environment string

// Preconfigured parameters
@description('Existing resource group init name to use in the deployment.')
param resourceGroupInitName string

@description('Existing managed identity name to use on load certificate.')
param managedIdentityName string

@description('Existing key vault secret id where is certificate stored.')
@secure()
param certificateSSL string

@description('Virtual network segment.')
param virtualNetworkSegment int 

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

@description('Workspace sku name that contain application insights.')
@allowed([
  'PerGB2018'
  'Premium'
  'Standard'
])
param workSpaceSkuName string

@description('Service plan sku name that contain application services.')
@allowed([
  'B1'
  'P1V2'
  'P2V2'
])
param appServicePlanSkuName string

@description('SQL logical server administrator principal type.')
@allowed([
  'Group'
  'User'
])
param sqlServerAdminType string

@description('SQL database sku name.')
@allowed([
  'Basic'
  'S2'
  'P1'
])
param sqlDataBaseSkuName string

@description('Azure storage account sku name.')
@allowed([
  'Standard_LRS'
  'Standard_GRS'
])
param storageAccountSkuName string

@description('Public IP address sku name related to application gateway.')
@allowed([
  'Standard'
  'Basic'
])
param publicIPAddressSkuName string

@description('Public IP address version related to application gateway.')
@allowed([
  'IPv4'
  'IPv6'
])
param publicIPAddressVersion string

@description('Public IP allocation method related to application gateway.')
@allowed([
  'Static'
  'Dynamic'
])
param publicIPAllocationMethod string

@description('Application gateway sku name.')
@allowed([
  'Standard_v2'
  'WAF_v2'
])
param applicationGateWaySkuName string

// Naming parameters
@description('Virtual network name related to application gateway.')
param virtualNetworkName string = 'vnet-${prefix}-${environment}-${suffix}'

@description('Workspace name that contain application insights.')
param workSpaceName string = 'log-${prefix}-${environment}-${suffix}'

@description('Application insights name for monitoring the application services.')
param appInsightsName string = 'appi-${prefix}-${environment}-${suffix}'

@description('Service plan name that contain application services.')
param appServicePlanName string = 'asp-${prefix}-windows-${environment}-${suffix}'

@description('Application service name for api.')
param appServiceApiName string = 'app-${prefix}-api-${environment}-${suffix}'

@description('Application service name for server.')
param appServiceAppName string = 'app-${prefix}-app-${environment}-${suffix}'

@description('SQL logical server name.')
param sqlServerName string = 'sql-${prefix}-${environment}-${suffix}'

@description('SQL logical server replica name.')
param sqlServerReplicaName string = 'sql-${prefix}-replica-${environment}-${suffix}'

@description('SQL database name.')
param sqlDataBaseName string = 'sqldb-${prefix}-${environment}'

@description('Private sql endpoint name')
param privateEndpointSqlName string = 'pe-${prefix}-sql-${environment}-${suffix}'

@description('Private sql endpoint replica name')
param privateEndpointSqlReplicaName string = 'pe-${prefix}-sql-replica-${environment}-${suffix}'

@description('Private link database name')
param privateDnsZoneSqlName string = 'privatelink.database.windows.net'

@description('Azure storage account name.')
param storageAccountName string = 'st${prefix}${environment}${suffix}'

@description('Blob container name.')
param blobContainerName string = 'containers'

@description('Private blob endpoint name')
param privateEndpointBlobName string = 'pe-${prefix}-blob-${environment}-${suffix}'

@description('Private link storage account name')
param privateDnsZoneBlobName string = 'privatelink.blob.core.windows.net'

@description('File share name. File share names must be between 3 and 63 characters in length and use numbers, lower-case letters and dash (-) only.')
param fileShareName string = 'files'

@description('Private file endpoint name')
param privateEndpointFileName string = 'pe-${prefix}-file-${environment}-${suffix}'

@description('Private link storage account name')
param privateDnsZoneFileName string = 'privatelink.file.core.windows.net'

@description('Public IP address name related to application gateway.')
param publicIPAddressName string = 'pip-${prefix}-${environment}-${suffix}'

@description('Application gateway name.')
param applicationGateWayName string = 'agw-${prefix}-${environment}-${suffix}'

// Modules

module virtualNetworks 'modules/virtualNetworks.bicep' = {
  name: 'virtualNetworks'
  params: {
    resourceLocation: resourceLocation
    virtualNetworkName: virtualNetworkName
    virtualNetworkSegment: virtualNetworkSegment
  }
}

module appInsights 'modules/appInsights.bicep' = {
  name: 'appInsights'
  params: {
    appInsightsName: appInsightsName
    environment: environment
    resourceLocation: resourceLocation
    workSpaceSkuName: workSpaceSkuName
    workSpaceName: workSpaceName
  }
}

module appServices 'modules/appServices.bicep' = {
  name: 'appServices'
  params: {
    appInsightConnectionString: appInsights.outputs.connectionString
    appInsightInstrumentationKey: appInsights.outputs.instrumentationKey
    appServiceApiName: appServiceApiName
    appServiceAppName: appServiceAppName
    appServicePlanName: appServicePlanName
    appServicePlanSkuName: appServicePlanSkuName
    computeSubnetId: virtualNetworks.outputs.computeSubnetId
    environment: environment
    resourceLocation: resourceLocation
  }
}

module sqlDatabases 'modules/sqlDatabases.bicep' = {
  name: 'sqlDatabases'
  params: {
    dataReplicaSubnetId: virtualNetworks.outputs.dataReplicaSubnetId 
    dataSubnetId: virtualNetworks.outputs.dataSubnetId
    environment: environment
    globalLocation: globalLocation
    privateDnsZoneSqlName: privateDnsZoneSqlName
    privateEndpointSqlName: privateEndpointSqlName
    privateEndpointSqlReplicaName: privateEndpointSqlReplicaName
    replicaLocation: replicaLocation
    resourceLocation: resourceLocation
    sqlDataBaseName: sqlDataBaseName
    sqlDataBaseSkuName: sqlDataBaseSkuName
    sqlServerAdminLogin: sqlServerAdminLogin
    sqlServerAdminPassword: sqlServerAdminPassword
    sqlServerAdminSID: sqlServerAdminSID
    sqlServerAdminType: sqlServerAdminType
    sqlServerAdminUser: sqlServerAdminUser
    sqlServerName: sqlServerName
    sqlServerReplicaName: sqlServerReplicaName
    virtualNetworkId: virtualNetworks.outputs.virtualNetworkId
    virtualNetworkName: virtualNetworks.outputs.virtualNetworkName
  }
}

module storageAccounts 'modules/storageAccounts.bicep' = {
  name: 'storageAccounts'
  params: {
    blobContainerName: blobContainerName
    environment: environment
    fileShareName: fileShareName
    globalLocation: globalLocation
    storageAccountName: storageAccountName 
    storageAccountSkuName: storageAccountSkuName
    storageSubnetId: virtualNetworks.outputs.storageSubnetId
    privateEndpointBlobName:privateEndpointBlobName
    privateEndpointFileName:privateEndpointFileName
    privateDnsZoneBlobName: privateDnsZoneBlobName
    privateDnsZoneFileName: privateDnsZoneFileName
    resourceLocation: resourceLocation
    virtualNetworkId: virtualNetworks.outputs.virtualNetworkId
    virtualNetworkName: virtualNetworks.outputs.virtualNetworkName
  }
}

module appGateways 'modules/appGateways.bicep' = {
  name: 'appGateways'
  params: {
    appGatewaySubnetId: virtualNetworks.outputs.appGatewaySubnetId
    applicationGateWayName: applicationGateWayName
    applicationGateWaySkuName: applicationGateWaySkuName
    appServiceAppHostName: appServices.outputs.appHostName
    certificateSSL: certificateSSL
    environment: environment
    managedIdentityName: managedIdentityName
    publicIPAddressName: publicIPAddressName
    publicIPAddressSkuName: publicIPAddressSkuName
    publicIPAddressVersion: publicIPAddressVersion
    publicIPAllocationMethod: publicIPAllocationMethod
    resourceGroupInitName: resourceGroupInitName
    resourceLocation: resourceLocation
  }
  dependsOn: [
    virtualNetworks
    appServices
    sqlDatabases
    storageAccounts
  ]
}

output appHostName string = appServices.outputs.appName
output apiHostName string = appServices.outputs.apiName
output sqlServerName string = sqlDatabases.outputs.serverFQDN
output sqlDatabaseName string = sqlDatabases.outputs.databaseName
