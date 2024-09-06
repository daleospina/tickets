// Parameters
@description('Location for all resources.')
param resourceLocation string

@description('Environment for all resources.')
@allowed([
  'test'
  'prod'
])
param environment string

@description('Public IP address name related to application gateway.')
param publicIPAddressName string

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

@description('Application gateway name.')
param applicationGateWayName string 

@description('Application gateway sku name.')
@allowed([
  'Standard_v2'
  'WAF_v2'
])
param applicationGateWaySkuName string

@description('Existing subnet for application gateway.')
param appGatewaySubnetId string

@description('Existing resource group init name to use in the deployment.')
param resourceGroupInitName string

@description('Existing managed identity name to use on load certificate.')
param managedIdentityName string

@description('Existing key vault secret id where is certificate stored.')
@secure()
param certificateSSL string

@description('Existing application service server host name assigned to backend pools gateway.')
param appServiceAppHostName string

// Variables
var sslCertificateName = 'sslappgateway'
var gatewayIPConfigurationName = 'appGWPublicIpIPv4'
var frontendIPConfigurationName = 'appGWFrontendIpIPv4'
var port443Name = 'port_443'
var port80Name = 'port_80'
var backendPoolName = 'backendpool'
var httpsBackendSettingName = 'httpsbackendsetting'
var httpBackendSettingName = 'httpbackendsetting'
var httpsListenerName = 'httpslistener'
var httpListenerName = 'httplistener'
var httpsRoutingRuleName = 'httpsroutingrule'
var httpRoutingRuleName = 'httproutingrule'
var managedIdentityId = resourceId(resourceGroupInitName, 'Microsoft.ManagedIdentity/userAssignedIdentities',managedIdentityName)

resource publicIPAddress 'Microsoft.Network/publicIPAddresses@2023-09-01' = if(environment=='prod') {
  name: publicIPAddressName
  location: resourceLocation
  sku: {
    name: publicIPAddressSkuName
  }
  properties: {
    publicIPAddressVersion: publicIPAddressVersion
    publicIPAllocationMethod: publicIPAllocationMethod
  }
}

resource applicationGateWay 'Microsoft.Network/applicationGateways@2023-09-01' = if(environment=='prod') {
  name: applicationGateWayName
  location: resourceLocation
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedIdentityId}' : {}
    }
  }
  properties: {
    enableHttp2: false
    autoscaleConfiguration: {
      minCapacity: 0
      maxCapacity: 10
    }
    sku: {
      name: applicationGateWaySkuName
      tier: applicationGateWaySkuName
    }
    sslCertificates: [
      {
        name: sslCertificateName
        properties: {
          keyVaultSecretId: certificateSSL
        }
      }
    ]
    gatewayIPConfigurations: [
      {
        name: gatewayIPConfigurationName
        properties: {
          subnet: {
            id: appGatewaySubnetId
          }
        }
      }
    ]
    frontendIPConfigurations: [
      {
        name: frontendIPConfigurationName
        properties: {
          privateIPAllocationMethod: 'Dynamic'
          publicIPAddress: {
            id: publicIPAddress.id
          }
        }
      }
    ]
    frontendPorts: [
      {
        name: port443Name
        properties: {
          port: 443
        }
      }
      {
        name: port80Name
        properties: {
          port: 80
        }
      }
    ]
    backendAddressPools: [
      {
        name: backendPoolName
        properties: {
          backendAddresses: [
            {
              fqdn: appServiceAppHostName
            }
          ]
        }
      }
    ]
    backendHttpSettingsCollection: [
      {
        name: httpsBackendSettingName
        properties: {
          port: 443
          protocol: 'Https'
          cookieBasedAffinity: 'Disabled'
          pickHostNameFromBackendAddress: true
          requestTimeout: 600
        }
      }
      {
        name: httpBackendSettingName
        properties: {
          port: 80
          protocol: 'Http'
          cookieBasedAffinity: 'Disabled'
          pickHostNameFromBackendAddress: true
          requestTimeout: 600
        }
      }
    ]
    httpListeners: [
      {
        name: httpsListenerName
        properties: {
          frontendIPConfiguration: {
            id: resourceId('Microsoft.Network/applicationGateways/frontendIPConfigurations', applicationGateWayName, frontendIPConfigurationName)
          }
          frontendPort: {
            id: resourceId('Microsoft.Network/applicationGateways/frontendPorts', applicationGateWayName, port443Name)
          }
          protocol: 'Https'
          requireServerNameIndication: false
          sslCertificate: {
            id: resourceId('Microsoft.Network/applicationGateways/sslCertificates', applicationGateWayName, sslCertificateName)
          }
        }
      }
      {
        name: httpListenerName
        properties: {
          frontendIPConfiguration: {
            id: resourceId('Microsoft.Network/applicationGateways/frontendIPConfigurations', applicationGateWayName, frontendIPConfigurationName)
          }
          frontendPort: {
            id: resourceId('Microsoft.Network/applicationGateways/frontendPorts', applicationGateWayName, port80Name)
          }
          protocol: 'Http'
          requireServerNameIndication: false
        }
      }
    ]
    requestRoutingRules: [
      {
        name: httpsRoutingRuleName
        properties: {
          ruleType: 'Basic'
          priority: 10
          httpListener: {
            id: resourceId('Microsoft.Network/applicationGateways/httpListeners', applicationGateWayName, httpsListenerName)
          }
          backendAddressPool: {
            id: resourceId('Microsoft.Network/applicationGateways/backendAddressPools', applicationGateWayName, backendPoolName)
          }
          backendHttpSettings: {
            id: resourceId('Microsoft.Network/applicationGateways/backendHttpSettingsCollection', applicationGateWayName, httpsBackendSettingName)
          }
        }
      }
      {
        name: httpRoutingRuleName
        properties: {
          ruleType: 'Basic'
          priority: 20
          httpListener: {
            id: resourceId('Microsoft.Network/applicationGateways/httpListeners', applicationGateWayName, httpListenerName)
          }
          backendAddressPool: {
            id: resourceId('Microsoft.Network/applicationGateways/backendAddressPools', applicationGateWayName, backendPoolName)
          }
          backendHttpSettings: {
            id: resourceId('Microsoft.Network/applicationGateways/backendHttpSettingsCollection', applicationGateWayName, httpBackendSettingName)
          }
        }
      }
    ]
  }
}

// Outputs
