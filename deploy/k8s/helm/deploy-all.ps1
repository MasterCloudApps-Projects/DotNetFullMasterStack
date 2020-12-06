
Param(
    [parameter(Mandatory=$false)][string]$appName="dotnetfullmasterstack",
    [parameter(Mandatory=$false)][string]$domain="d7fc52a6d2b948eaa635.westeurope.aksapp.io",
    [parameter(Mandatory=$false)][bool]$debughelm=$false
    )

function Install-Chart  {
    Param([string]$chart)
    
    $options = "upgrade --install --set application.name=$appName --set application.domain=$domain $appName-$chart $chart --namespace=$appName" 
    
    if ($debughelm) {
        $options = $options + " --dry-run --debug ";
    }

    Write-Host "Helm Command: helm3 $options" -ForegroundColor Gray
    Invoke-Expression 'cmd /c "helm3 $options"'
}

$charts = (
    "admin-service",
    "config-service",
    "customer-api",
    "eureka-service",
    "gateway-service",
    "kitchen-api",
    "ordering-api",
    "restaurant-api",
    "mssql-linux",
    "ui-aspnet-blazor"
)


foreach ($chart in $charts) {
    Write-Host "Installing: $chart" -ForegroundColor Green
    Install-Chart $chart 
}
