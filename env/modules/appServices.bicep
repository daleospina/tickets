// Parameters
@description('Location for all resources.')
param resourceLocation string

@description('Service plan name that contain application services.')
param appServicePlanName string 

@description('Service plan sku name that contain application services.')
@allowed([
  'B1'
  'P1V2'
  'P2V2'
])
param appServicePlanSkuName string

@description('Application service name for api.')
param appServiceApiName string

@description('Application service name for server.')
param appServiceAppName string 

@description('Existing application insight instrumentation key for assign to application service.')
param appInsightInstrumentationKey string

@description('Existing application insight connection string  for assign to application service.')
param appInsightConnectionString string

@description('Existing subnet for compute services.')
param computeSubnetId string

// Variables

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: appServicePlanName
  location: resourceLocation
  sku: {
    name: appServicePlanSkuName
  }
}

resource appServiceApi 'Microsoft.Web/sites@2023-01-01' = {
  name: appServiceApiName
  location: resourceLocation
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    virtualNetworkSubnetId: computeSubnetId
    vnetRouteAllEnabled: true
    siteConfig:{
      appSettings:[
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value:  appInsightInstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsightConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_Mode'
          value: 'recommended'
        }
        {
          name: 'SCM_DO_BUILD_DURING_DEPLOYMENT'
          value: 'true'
        }
      ]
    }
  }
  resource slotConfig 'config@2023-01-01' = {
    name: 'slotConfigNames'
    properties: {
      appSettingNames: [
        'APPINSIGHTS_INSTRUMENTATIONKEY'
        'APPLICATIONINSIGHTS_CONNECTION_STRING'
      ]
    }
  }
}

resource appServiceApp 'Microsoft.Web/sites@2023-01-01' = {
  name: appServiceAppName
  location: resourceLocation
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    virtualNetworkSubnetId: computeSubnetId
    vnetRouteAllEnabled: true
    siteConfig:{
      appSettings:[
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value:  appInsightInstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsightConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_Mode'
          value: 'recommended'
        }
        {
          name: 'SCM_DO_BUILD_DURING_DEPLOYMENT'
          value: 'true'
        }
      ]
    }
  }
  resource slotConfig 'config@2023-01-01' = {
    name: 'slotConfigNames'
    properties: {
      appSettingNames: [
        'APPINSIGHTS_INSTRUMENTATIONKEY'
        'APPLICATIONINSIGHTS_CONNECTION_STRING'
      ]
    }
  }
}

// Outputs
output apiName string = appServiceApi.name
output apiHostName string = appServiceApi.properties.defaultHostName
output appName string = appServiceApp.name
output appHostName string = appServiceApp.properties.defaultHostName
