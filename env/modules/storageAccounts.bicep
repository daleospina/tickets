// Parameters
@description('Location for all resources.')
param resourceLocation string

@description('Global location name')
param globalLocation string

@description('Azure storage account name.')
param storageAccountName string 

@description('Azure storage account sku name.')
@allowed([
  'Standard_LRS'
  'Standard_ZRS'
  'Standard_GRS'
])
param storageAccountSkuName string

@description('Blob container name.')
param blobContainerName string

@description('File share name. File share names must be between 3 and 63 characters in length and use numbers, lower-case letters and dash (-) only.')
param fileShareName string

@description('Private endpoint name')
param privateEndpointBlobName string

@description('Private endpoint name')
param privateEndpointFileName string

@description('Private blob dns zone name')
param privateDnsZoneBlobName string

@description('Private file dns zone name')
param privateDnsZoneFileName string

@description('Existing subnet for file storages.')
param storageSubnetId string

@description('Existing virtual network.')
param virtualNetworkId string

@description('Existing virtual network.')
param virtualNetworkName string

// Variables

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountName
  location: resourceLocation
  identity: {
    type: 'SystemAssigned'
  }
  kind: 'StorageV2'
  sku: {
    name: storageAccountSkuName
  }
  properties: {
    accessTier: 'Hot'
    publicNetworkAccess: 'Enabled'
    supportsHttpsTrafficOnly: true
    networkAcls: {
      bypass: 'None'
      defaultAction: 'Allow'
    }
    minimumTlsVersion: 'TLS1_2'
  }
}

resource blobService 'Microsoft.Storage/storageAccounts/blobServices@2023-05-01' = {
  name: 'default'
  parent: storageAccount
}

resource blobContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-05-01' = {
  name: blobContainerName
  parent: blobService
}

resource fileService 'Microsoft.Storage/storageAccounts/fileServices@2023-05-01' = {
  name: 'default'
  parent: storageAccount
}

resource fileShare 'Microsoft.Storage/storageAccounts/fileServices/shares@2023-05-01' = {
  name: fileShareName
  parent: fileService
}

resource privateEndpointBlob 'Microsoft.Network/privateEndpoints@2023-09-01' = {
  name: privateEndpointBlobName
  location: resourceLocation
  properties: {
    subnet: {
      id: storageSubnetId
    }
    customNetworkInterfaceName: '${privateEndpointBlobName}-nic'
    privateLinkServiceConnections: [
      {
        name: privateEndpointBlobName
        properties: {
          privateLinkServiceId: storageAccount.id
          groupIds: [
            'blob'
          ]
        }
      }
    ]
  }
}

resource privateEndpointFile 'Microsoft.Network/privateEndpoints@2023-09-01' = {
  name: privateEndpointFileName
  location: resourceLocation
  properties: {
    subnet: {
      id: storageSubnetId
    }
    customNetworkInterfaceName: '${privateEndpointFileName}-nic'
    privateLinkServiceConnections: [
      {
        name: privateEndpointFileName
        properties: {
          privateLinkServiceId: storageAccount.id
          groupIds: [
            'file'
          ]
        }
      }
    ]
  }
}

resource privateDnsZoneBlob 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: privateDnsZoneBlobName
  location: globalLocation
}

resource privateDnsZoneFile 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: privateDnsZoneFileName
  location: globalLocation
}

resource privateDnsZoneLinkBlob 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZoneBlob
  name: '${privateDnsZoneBlobName}-${virtualNetworkName}'
  location: globalLocation
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkId
    }
  }
}

resource privateDnsZoneLinkFile 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZoneFile
  name: '${privateDnsZoneFileName}-${virtualNetworkName}'
  location: globalLocation
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkId
    }
  }
}

resource privateDnsZoneGroupBlob 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-09-01' = {
  parent: privateEndpointBlob
  name: 'default'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'default'
        properties: {
          privateDnsZoneId: privateDnsZoneBlob.id
        }
      }
    ]
  }
}

resource privateDnsZoneGroupFile 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-09-01' = {
  parent: privateEndpointFile
  name: 'default'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'default'
        properties: {
          privateDnsZoneId: privateDnsZoneFile.id
        }
      }
    ]
  }
}

// Outputs
output id string = storageAccount.id
