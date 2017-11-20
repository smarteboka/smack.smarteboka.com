 var project = "./source/smack.smarteboka.com/smack.smarteboka.com.csproj";
              
Task("Build")
    .Does(() => {
        DotNetCoreBuild(project);
});

Task("Publish")
    .Does(() => {
    var settings = new DotNetCorePublishSettings
    {
        Configuration = "Release",
        OutputDirectory = "./publish/"
    };

     DotNetCorePublish(project, settings);
});

Task("Deploy")
    .IsDependentOn("Publish")
    .Does(() =>
    {
        var dir = "deploy";
        if (!DirectoryExists(dir))        {
            CreateDirectory(dir);
        }else{
            CleanDirectory(dir);
        }

        var deployZip = "./deploy/deploypackage.zip";
        Zip("./publish", deployZip , "./publish/**/*");
        var siteName = "smack-smarteboka-com";
        var zipPushUrl = $"https://{siteName}.scm.azurewebsites.net/api/zipdeploy";
        var user = EnvironmentVariable("AzureDeployCredentials");
        var curlParams = $"-u {user} --data-binary \"@{deployZip}\" --url {zipPushUrl}";
        StartProcess("curl", curlParams);
    });

Task("Default")
    .Does(() =>
{
    Information("Running default");
});

RunTarget("Deploy");