// Parameters
@description('Location for all resources.')
param resourceLocation string

@description('Workspace name that contain application insights.')
param workSpaceName string

@description('Workspace sku name that contain application insights.')
@allowed([
  'PerGB2018'
  'Premium'
  'Standard'
])
param workSpaceSkuName string

@description('Application insights name for monitoring the application services.')
param appInsightsName string

// Variables

resource workSpace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: workSpaceName
  location: resourceLocation
  properties: {
    sku: {
      name: workSpaceSkuName
    }
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02'= {
  name: appInsightsName
  location: resourceLocation
  kind: 'other'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Bluefield'
    WorkspaceResourceId: workSpace.id
    RetentionInDays: 90
    IngestionMode: 'LogAnalytics'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

// Outputs
output appInsightsId string = appInsights.id
output appInsightsName string = appInsights.name
output instrumentationKey string = appInsights.properties.InstrumentationKey
output connectionString string = appInsights.properties.ConnectionString
