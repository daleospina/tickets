// Parameters
@description('Location for all resources.')
param resourceLocation string

@description('Virtual network name related to application gateway.')
param virtualNetworkName string 

@description('Virtual network segment.')
param virtualNetworkSegment int 

// Variables
var appGatewaySubnetName = 'agsubnet'
var computeSubnetName = 'computesubnet'
var dataSubnetName = 'datasubnet'
var dataReplicaSubnetName = 'datareplicasubnet'
var storageSubnetName = 'storagesubnet'

resource vNet 'Microsoft.Network/virtualNetworks@2023-09-01' = {
  name: virtualNetworkName
  location: resourceLocation
  properties: {
    addressSpace: {
      addressPrefixes: [
        '10.${virtualNetworkSegment}.0.0/16'
      ]
    }
    subnets: [
      {
        name: appGatewaySubnetName
        properties: {
          addressPrefix: '10.${virtualNetworkSegment}.0.0/24'
        }
      }
      {
        name: computeSubnetName
        properties: {
          addressPrefix: '10.${virtualNetworkSegment}.1.0/24'
          delegations: [
            {
              name: 'delegation'             
              properties: {
                serviceName: 'Microsoft.Web/serverfarms'
              }
              type: 'Microsoft.Network/virtualNetworks/subnets/delegations'
            }
          ]
        }
      }
      {
        name: dataSubnetName
        properties: {
          addressPrefix: '10.${virtualNetworkSegment}.2.0/24'
        }
      }
      {
        name: dataReplicaSubnetName
        properties: {
          addressPrefix: '10.${virtualNetworkSegment}.3.0/24'
        }
      }
      {
        name: storageSubnetName
        properties: {
          addressPrefix: '10.${virtualNetworkSegment}.4.0/24'
        }
      }
    ] 
  }

  resource appGatewaySubnet 'subnets' existing = {
    name: appGatewaySubnetName
  }

  resource computeSubnet 'subnets' existing = {
    name: computeSubnetName
  }

  resource dataSubnet 'subnets' existing = {
    name: dataSubnetName
  }

  resource dataReplicaSubnet 'subnets' existing = {
    name: dataReplicaSubnetName
  }

  resource storageSubnet 'subnets' existing = {
    name: storageSubnetName
  }
}

// Outputs
output virtualNetworkId string = vNet.id
output virtualNetworkName string = vNet.name
output appGatewaySubnetId string =  vNet::appGatewaySubnet.id
output appGatewaySubnetName string = vNet::appGatewaySubnet.name
output computeSubnetId string = vNet::computeSubnet.id
output computeSubnetName string = vNet::computeSubnet.name
output dataSubnetId string = vNet::dataSubnet.id
output dataSubnetName string = vNet::dataSubnet.name
output dataReplicaSubnetId string = vNet::dataReplicaSubnet.id
output dataReplicaSubnetName string = vNet::dataReplicaSubnet.name
output storageSubnetId string = vNet::storageSubnet.id
output storageSubnetName string = vNet::storageSubnet.name
