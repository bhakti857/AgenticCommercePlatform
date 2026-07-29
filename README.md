Microsoft Windows [Version 10.0.26200.8875]
(c) Microsoft Corporation. All rights reserved.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new sln -n AI-Ecommerce-Platform
The template "Solution File" was created successfully.


C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new webapi -n src/AI-Ecommerce.Api --framework net9.0
Error: Invalid option(s):
--framework net9.0
   'net9.0' is not a valid value for --framework. The possible values are:
      net10.0   - Target net10.0
      net7.0    - Target net7.0
      net8.0    - Target net8.0

For more information, run:
   dotnet new webapi -h

For details on the exit code, refer to https://aka.ms/templating-exit-codes#127

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new webapi -h
ASP.NET Core Web API (C#)
Author: Microsoft
Description: A project template for creating a RESTful Web API using ASP.NET Core controllers or minimal APIs, with optional support for OpenAPI and authentication.

Usage:
  dotnet new webapi [options] [template options]

Options:
  -n, --name <name>       The name for the output being created. If no name is specified, the name of the output
                          directory is used.
  -o, --output <output>   Location to place the generated output.
  --dry-run               Displays a summary of what would happen if the given command line were run if it would result
                          in a template creation. [default: False]
  --force                 Forces content to be generated even if it would change existing files. [default: False]
  --no-update-check       Disables checking for the template package updates when instantiating a template. [default:
                          False]
  --project <project>     The project that should be used for context evaluation.
  -lang, --language <C#>  Specifies the template language to instantiate.
  --type <project>        Specifies the template type to instantiate.

Template options:
  -au, --auth <None|IndividualB2C|...>     The type of authentication to use
                                           Type: choice
                                             None           No authentication
                                             IndividualB2C  Individual authentication with Azure AD B2C
                                             SingleOrg      Organizational authentication for a single tenant
                                             Windows        Windows authentication
                                           Default: None
  --aad-b2c-instance <aad-b2c-instance>    The Azure Active Directory B2C instance to connect to (use with
                                           IndividualB2C auth).
                                           Type: string
                                           Default: https://qualified.domain.name.b2clogin.com/
  -ssp, --susi-policy-id <susi-policy-id>  The sign-in and sign-up policy ID for this project (use with IndividualB2C
                                           auth).
                                           Type: string
                                           Default: b2c_1_susi
  --aad-instance <aad-instance>            The Azure Active Directory instance to connect to (use with SingleOrg auth).
                                           Type: string
                                           Default: https://login.microsoftonline.com/
  --client-id <client-id>                  The Client ID for this project (use with SingleOrg or IndividualB2C auth).
                                           Type: string
                                           Default: 11111111-1111-1111-11111111111111111
  --domain <domain>                        The domain for the directory tenant (use with SingleOrg or IndividualB2C
                                           auth).
                                           Type: string
                                           Default: qualified.domain.name
  --default-scope <default-scope>          The API scope the client needs to request to provision an access token. (use
                                           with IndividualB2C, SingleOrg).
                                           Type: string
                                           Default: access_as_user
  --tenant-id <tenant-id>                  The TenantId ID of the directory to connect to (use with SingleOrg auth).
                                           Type: string
                                           Default: 22222222-2222-2222-2222-222222222222
  -r, --org-read-access                    Whether or not to allow this application read access to the directory (only
                                           applies to SingleOrg auth).
                                           Type: bool
                                           Default: false
  --exclude-launch-settings                Whether to exclude launchSettings.json in the generated template.
                                           Type: bool
                                           Default: false
  --no-https                               Whether to turn off HTTPS. This option only applies if IndividualB2C,
                                           SingleOrg, or MultiOrg aren't used for --auth.
                                           Type: bool
                                           Default: false
  -uld, --use-local-db                     Whether to use LocalDB instead of SQLite. This option only applies if --auth
                                           Individual or --auth IndividualB2C is specified.
                                           Type: bool
                                           Default: false
  -f, --framework <net10.0|net7.0|net8.0>  The target framework for the project.
                                           Type: choice
                                             net10.0  Target net10.0
                                             net8.0   Target net8.0
                                             net7.0   Target net7.0
                                           Default: net10.0
  --no-restore                             If specified, skips the automatic restore of the project on create.
                                           Type: bool
                                           Default: false
  --called-api-url <called-api-url>        URL of the API to call from the web app. This option only applies if --auth
                                           SingleOrg or --auth IndividualB2C is specified.
                                           Type: string
                                           Default: https://graph.microsoft.com/v1.0
  --calls-graph                            Specifies if the web app calls Microsoft Graph. This option only applies if
                                           --auth SingleOrg is specified.
                                           Type: bool
                                           Default: false
  --called-api-scopes <called-api-scopes>  Scopes to request to call the API from the web app. This option only applies
                                           if --auth SingleOrg or --auth IndividualB2C is specified.
                                           Type: string
                                           Default: user.read
  --no-openapi                             Disable OpenAPI (Swagger) support
                                           Type: bool
                                           Default: false
  --use-program-main                       Whether to generate an explicit Program class and Main method instead of
                                           top-level statements.
                                           Type: bool
                                           Default: false
  -controllers, --use-controllers          Whether to use controllers instead of minimal APIs. This option overides the
                                           value specified by --minimal.
                                           Type: bool
                                           Default: false
  -minimal, --use-minimal-apis             Whether to use minimal APIs instead of controllers.
                                           Type: bool
                                           Default: false

To see help for other template languages (F#), use --language option:
   dotnet new webapi -h --language F#


C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new webapi -n src/AI-Ecommerce.Api --framework net8.0
Name cannot contain any of the following characters '"', '<', '>', '|', ':', '*', '?', '\', '/' or character codes char(0), char(1), char(2), char(3), char(4), char(5), char(6), char(7), char(8), char(9), char(10), char(11), char(12), char(13), char(14), char(15), char(16), char(17), char(18), char(19), char(20), char(21), char(22), char(23), char(24), char(25), char(26), char(27), char(28), char(29), char(30), char(31)


For details on the exit code, refer to https://aka.ms/templating-exit-codes#127

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new webapi -n AI-Ecommerce.Api -o src/AI-Ecommerce.Api --framework net8.0
The template "ASP.NET Core Web API" was created successfully.

Processing post-creation actions...
Restoring C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj:
Restore succeeded.



C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new classlib -n AI-Ecommerce.Data -o src/AI-Ecommerce.Data --framework net8.0
The template "Class Library" was created successfully.

Processing post-creation actions...
Restoring C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj:
Restore succeeded.



C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new classlib -n AI-Ecommerce.Agent -o src/AI-Ecommerce.Agent --framework net8.0
The template "Class Library" was created successfully.

Processing post-creation actions...
Restoring C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj:
Restore succeeded.



C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet new xunit -n AI-Ecommerce.Tests -o tests/AI-Ecommerce.Tests --framework net8.0
The template "xUnit Test Project" was created successfully.

Processing post-creation actions...
Restoring C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\tests\AI-Ecommerce.Tests\AI-Ecommerce.Tests.csproj:
Restore succeeded.



C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet sln add src/**/*.csproj tests/**/*.csproj
Could not find project or directory `src/**/*.csproj`.
Could not find project or directory `tests/**/*.csproj`.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet sln add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj
Project `src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj` added to the solution.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet sln add src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj
Project `src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj` added to the solution.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet sln add src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj
Project `src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj` added to the solution.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet sln add tests/AI-Ecommerce.Tests/AI-Ecommerce.Tests.csproj
Project `tests\AI-Ecommerce.Tests\AI-Ecommerce.Tests.csproj` added to the solution.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj reference src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj
Reference `..\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj` added to the project.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj reference src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj
Reference `..\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj` added to the project.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj reference src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj
Reference `..\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj` added to the project.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add tests/AI-Ecommerce.Tests/AI-Ecommerce.Tests.csproj reference src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj
Reference `..\..\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj` added to the project.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add tests/AI-Ecommerce.Tests/AI-Ecommerce.Tests.csproj reference src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj
Reference `..\..\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj` added to the project.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore complete (3.3s)

Build succeeded in 3.6s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore complete (1.7s)
  AI-Ecommerce.Data net8.0 succeeded (11.8s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 succeeded (2.0s) → src\AI-Ecommerce.Agent\bin\Debug\net8.0\AI-Ecommerce.Agent.dll
  AI-Ecommerce.Api net8.0 succeeded (8.1s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
  AI-Ecommerce.Tests net8.0 succeeded (3.7s) → tests\AI-Ecommerce.Tests\bin\Debug\net8.0\AI-Ecommerce.Tests.dll

Build succeeded in 26.0s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>Project `src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj` added to the solution.
'Project' is not recognized as an internal or external command,
operable program or batch file.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet sln list
Project(s)
----------
src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj
src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj
src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj
tests\AI-Ecommerce.Tests\AI-Ecommerce.Tests.csproj

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet ef migrations add InitialCreate --project src/AI-Ecommerce.Data --startup-project src/AI-Ecommerce.Api
Build started...
Build succeeded.
Your startup project 'AI-Ecommerce.Api' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package Microsoft.EntityFrameworkCore.Design --version 8.0.0
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.EntityFrameworkCore.Design' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/index.json 420ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/8.0.0/microsoft.entityframeworkcore.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/8.0.0/microsoft.entityframeworkcore.8.0.0.nupkg 151ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/index.json 125ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/8.0.0/microsoft.entityframeworkcore.abstractions.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/index.json 212ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/8.0.0/microsoft.entityframeworkcore.analyzers.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/8.0.0/microsoft.entityframeworkcore.abstractions.8.0.0.nupkg 129ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/8.0.0/microsoft.entityframeworkcore.analyzers.8.0.0.nupkg 52ms
info : Installed Microsoft.EntityFrameworkCore.Analyzers 8.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.entityframeworkcore.analyzers\8.0.0 with content hash ZXxEeLs2zoZ1TA+QoMMcw4f3Tirf8PzgdDax8RoWo0dxI2KmqiEGWYjhm2B/XyWfglc6+mNRyB8rZiQSmxCpeg==.
info : Installed Microsoft.EntityFrameworkCore.Abstractions 8.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.entityframeworkcore.abstractions\8.0.0 with content hash VR22s3+zoqlVI7xauFKn1znSIFHO8xuILT+noSwS8bZCKcHz0ydkTDQMuaxSa5WBaQrZmwtTz9rmRvJ7X8mSPQ==.
info : Installed Microsoft.EntityFrameworkCore 8.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.entityframeworkcore\8.0.0 with content hash SoODat83pGQUpWB9xULdMX6tuKpq/RTXDuJ2WeC1ldUKcKzLkaFJD1n+I0nOLY58odez/e7z8b6zdp235G/kyg==.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
info : Package 'Microsoft.EntityFrameworkCore.Design' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'Microsoft.EntityFrameworkCore.Design' version '8.0.0' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\AI-Ecommerce.Api.csproj.nuget.g.props.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\AI-Ecommerce.Api.csproj.nuget.g.targets.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 4.32 sec).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore complete (2.2s)

Build succeeded in 2.5s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet ef migrations add InitialCreate --project src/AI-Ecommerce.Data --startup-project src/AI-Ecommerce.Api
Build started...
Build succeeded.
No DbContext was found in assembly 'AI-Ecommerce.Data'. Ensure that you're using the correct assembly and that the type is neither abstract nor generic.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>mkdir src\AI-Ecommerce.Data\Models

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.EntityFrameworkCore.SqlServer' into project 'src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.sqlserver/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.sqlserver/index.json 210ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.sqlserver/8.0.0/microsoft.entityframeworkcore.sqlserver.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.sqlserver/8.0.0/microsoft.entityframeworkcore.sqlserver.8.0.0.nupkg 122ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient/index.json 99ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient/5.1.1/microsoft.data.sqlclient.5.1.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient/5.1.1/microsoft.data.sqlclient.5.1.1.nupkg 78ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient.sni.runtime/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/azure.identity/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient.sni.runtime/index.json 133ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient.sni.runtime/5.1.0/microsoft.data.sqlclient.sni.runtime.5.1.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/azure.identity/index.json 201ms
info :   GET https://api.nuget.org/v3-flatcontainer/azure.identity/1.7.0/azure.identity.1.7.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/index.json 236ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/6.24.0/microsoft.identitymodel.jsonwebtokens.6.24.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/index.json 310ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/index.json 299ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.data.sqlclient.sni.runtime/5.1.0/microsoft.data.sqlclient.sni.runtime.5.1.0.nupkg 169ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/4.47.2/microsoft.identity.client.4.47.2.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/6.24.0/microsoft.identitymodel.protocols.openidconnect.6.24.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/azure.identity/1.7.0/azure.identity.1.7.0.nupkg 221ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/6.24.0/microsoft.identitymodel.jsonwebtokens.6.24.0.nupkg 211ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/6.24.0/microsoft.identitymodel.protocols.openidconnect.6.24.0.nupkg 298ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/4.47.2/microsoft.identity.client.4.47.2.nupkg 304ms
info :   GET https://api.nuget.org/v3-flatcontainer/azure.core/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/4.39.0/microsoft.identity.client.4.39.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identity.client.extensions.msal/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identity.client.extensions.msal/index.json 144ms
info :   OK https://api.nuget.org/v3-flatcontainer/azure.core/index.json 162ms
info :   GET https://api.nuget.org/v3-flatcontainer/azure.core/1.25.0/azure.core.1.25.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identity.client.extensions.msal/2.19.3/microsoft.identity.client.extensions.msal.2.19.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/4.39.0/microsoft.identity.client.4.39.0.nupkg 213ms
info :   OK https://api.nuget.org/v3-flatcontainer/azure.core/1.25.0/azure.core.1.25.0.nupkg 201ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identity.client.extensions.msal/2.19.3/microsoft.identity.client.extensions.msal.2.19.3.nupkg 281ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/index.json 52ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/index.json 50ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/index.json 41ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/6.24.0/microsoft.identitymodel.protocols.6.24.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/6.24.0/microsoft.identitymodel.tokens.6.24.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/6.24.0/system.identitymodel.tokens.jwt.6.24.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/index.json 133ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/6.24.0/system.identitymodel.tokens.jwt.6.24.0.nupkg 148ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/4.6.0/system.diagnostics.diagnosticsource.4.6.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/6.24.0/microsoft.identitymodel.protocols.6.24.0.nupkg 180ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/6.24.0/microsoft.identitymodel.tokens.6.24.0.nupkg 181ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/4.6.0/system.diagnostics.diagnosticsource.4.6.0.nupkg 241ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/4.38.0/microsoft.identity.client.4.38.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identity.client/4.38.0/microsoft.identity.client.4.38.0.nupkg 46ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/index.json 48ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/6.24.0/microsoft.identitymodel.logging.6.24.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/6.24.0/microsoft.identitymodel.logging.6.24.0.nupkg 102ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/index.json 61ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/6.24.0/microsoft.identitymodel.abstractions.6.24.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/6.24.0/microsoft.identitymodel.abstractions.6.24.0.nupkg 76ms
info : Installed Microsoft.EntityFrameworkCore.SqlServer 8.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.entityframeworkcore.sqlserver\8.0.0 with content hash GeOmafQn64HyQtYcI/Omv/D/YVHd1zEkWbP3zNQu4oC+usE9K0qOp0R8KgWWFEf8BU4tXuYbok40W0SjfbaK/A==.
info : Installed Microsoft.Data.SqlClient 5.1.1 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.data.sqlclient\5.1.1 with content hash MW5E9HFvCaV069o8b6YpuRDPBux8s96qDnOJ+4N9QNUCs7c5W3KxwQ+ftpAjbMUlImL+c9WR+l+f5hzjkqhu2g==.
info : Installed Azure.Identity 1.7.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\azure.identity\1.7.0 with content hash eHEiCO/8+MfNc9nH5dVew/+FvxdaGrkRL4OMNwIz0W79+wtJyEoeRlXJ3SrXhoy9XR58geBYKmzMR83VO7bcAw==.
info : Installed Microsoft.Identity.Client.Extensions.Msal 2.19.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identity.client.extensions.msal\2.19.3 with content hash zVVZjn8aW7W79rC1crioDgdOwaFTQorsSO6RgVlDDjc7MvbEGz071wSNrjVhzR0CdQn6Sefx7Abf1o7vasmrLg==.
info : Installed Azure.Core 1.25.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\azure.core\1.25.0 with content hash X8Dd4sAggS84KScWIjEbFAdt2U1KDolQopTPoHVubG2y3CM54f9l6asVrP5Uy384NWXjsspPYaJgz5xHc+KvTA==.
info : Installed System.IdentityModel.Tokens.Jwt 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.identitymodel.tokens.jwt\6.24.0 with content hash Qibsj9MPWq8S/C0FgvmsLfIlHLE7ay0MJIaAmK94ivN3VyDdglqReed5qMvdQhSL0BzK6v0Z1wB/sD88zVu6Jw==.
info : Installed Microsoft.IdentityModel.Abstractions 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.abstractions\6.24.0 with content hash X6aBK56Ot15qKyG7X37KsPnrwah+Ka55NJWPppWVTDi8xWq7CJgeNw2XyaeHgE1o/mW4THwoabZkBbeG2TPBiw==.
info : Installed Microsoft.IdentityModel.JsonWebTokens 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.jsonwebtokens\6.24.0 with content hash XDWrkThcxfuWp79AvAtg5f+uRS1BxkIbJnsG/e8VPzOWkYYuDg33emLjp5EWcwXYYIDsHnVZD/00kM/PYFQc/g==.
info : Installed Microsoft.IdentityModel.Protocols 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.protocols\6.24.0 with content hash +NzKCkvsQ8X1r/Ff74V7CFr9OsdMRaB6DsV+qpH7NNLdYJ8O4qHbmTnNEsjFcDmk/gVNDwhoL2gN5pkPVq0lwQ==.
info : Installed Microsoft.IdentityModel.Tokens 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.tokens\6.24.0 with content hash ZPqHi86UYuqJXJ7bLnlEctHKkPKT4lGUFbotoCNiXNCSL02emYlcxzGYsRGWWmbFEcYDMi2dcTLLYNzHqWOTsw==.
info : Installed Microsoft.IdentityModel.Logging 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.logging\6.24.0 with content hash qLYWDOowM/zghmYKXw1yfYKlHOdS41i8t4hVXr9bSI90zHqhyhQh9GwVy8pENzs5wHeytU23DymluC9NtgYv7w==.
info : Installed System.Diagnostics.DiagnosticSource 4.6.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.diagnostics.diagnosticsource\4.6.0 with content hash mbBgoR0rRfl2uimsZ2avZY8g7Xnh1Mza0rJZLPcxqiMWlkGukjmRkuMJ/er+AhQuiRIh80CR/Hpeztr80seV5g==.
info : Installed Microsoft.IdentityModel.Protocols.OpenIdConnect 6.24.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.protocols.openidconnect\6.24.0 with content hash a/2RRrc8C9qaw8qdD9hv1ES9YKFgxaqr/SnwMSLbwQZJSUQDd4qx1K4EYgWaQWs73R+VXLyKSxN0f/uE9CsBiQ==.
info : Installed Microsoft.Data.SqlClient.SNI.runtime 5.1.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.data.sqlclient.sni.runtime\5.1.0 with content hash jVsElisM5sfBzaaV9kdq2NXZLwIbytetnsOIlJ0cQGgQP4zFNBmkfHBnpwtmKrtBJBEV9+9PVQPVrcCVhDgcIg==.
info : Installed Microsoft.Identity.Client 4.39.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identity.client\4.39.0 with content hash +/y4ELXYqnAbiqhEgFAl3riBbkMeCw8+6+Y8r367bUf6zWhlNUjhm360VsTMBgqbPyfJld5X+cJkDhDCrudezA==.
info : Installed Microsoft.Identity.Client 4.38.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identity.client\4.38.0 with content hash fADpikF/MKzS7+aIpZXgXsKZHgILAQ6y6xD4iN7H49F4SvYGgg6CLPigN0zD656NrJd++MBIQh8sZ8nqomVhbw==.
info : Installed Microsoft.Identity.Client 4.47.2 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identity.client\4.47.2 with content hash SPgesZRbXoDxg8Vv7k5Ou0ee7uupVw0E8ZCc4GKw25HANRLz1d5OSr0fvTVQRnEswo5Obk8qD4LOapYB+n5kzQ==.
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 218ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json 278ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json 280ms
info : Package 'Microsoft.EntityFrameworkCore.SqlServer' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj'.
info : PackageReference for package 'Microsoft.EntityFrameworkCore.SqlServer' version '8.0.0' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj'.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\obj\AI-Ecommerce.Data.csproj.nuget.g.props.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\obj\AI-Ecommerce.Data.csproj.nuget.g.targets.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj (in 1.08 min).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.EntityFrameworkCore.Tools' into project 'src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.tools/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.tools/index.json 806ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.tools/8.0.0/microsoft.entityframeworkcore.tools.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.tools/8.0.0/microsoft.entityframeworkcore.tools.8.0.0.nupkg 191ms
info : Installed Microsoft.EntityFrameworkCore.Tools 8.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.entityframeworkcore.tools\8.0.0 with content hash zRdaXiiB1gEA0b+AJTd2+drh78gkEA4HyZ1vqNZrKq4xwW8WwavSiQsoeb1UsIMZkocLMBbhQYWClkZzuTKEgQ==.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
info : Package 'Microsoft.EntityFrameworkCore.Tools' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj'.
info : PackageReference for package 'Microsoft.EntityFrameworkCore.Tools' version '8.0.0' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj'.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\obj\AI-Ecommerce.Data.csproj.nuget.g.props.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\obj\AI-Ecommerce.Data.csproj.nuget.g.targets.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Data\AI-Ecommerce.Data.csproj (in 2.77 sec).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore complete (2.1s)

Build succeeded in 2.4s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore complete (1.2s)
  AI-Ecommerce.Data net8.0 succeeded (5.0s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 succeeded (1.0s) → src\AI-Ecommerce.Agent\bin\Debug\net8.0\AI-Ecommerce.Agent.dll
  AI-Ecommerce.Api net8.0 succeeded (2.6s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
  AI-Ecommerce.Tests net8.0 succeeded (1.8s) → tests\AI-Ecommerce.Tests\bin\Debug\net8.0\AI-Ecommerce.Tests.dll

Build succeeded in 11.5s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet ef migrations add InitialCreate --project src/AI-Ecommerce.Data --startup-project src/AI-Ecommerce.Api
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet ef database update --project src/AI-Ecommerce.Data --startup-project src/AI-Ecommerce.Api
Build started...
Build succeeded.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1,488ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      CREATE DATABASE [AgenticCommerceDB];
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (161ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      IF SERVERPROPERTY('EngineEdition') <> 5
      BEGIN
          ALTER DATABASE [AgenticCommerceDB] SET READ_COMMITTED_SNAPSHOT ON;
      END;
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (14ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (15ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [__EFMigrationsHistory] (
          [MigrationId] nvarchar(150) NOT NULL,
          [ProductVersion] nvarchar(32) NOT NULL,
          CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (21ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20260727104758_InitialCreate'.
Applying migration '20260727104758_InitialCreate'.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Products] (
          [Id] int NOT NULL IDENTITY,
          [SKU] nvarchar(50) NOT NULL,
          [Name] nvarchar(200) NOT NULL,
          [Description] nvarchar(max) NULL,
          [Price] decimal(18,2) NOT NULL,
          [Cost] decimal(18,2) NOT NULL,
          [Category] nvarchar(100) NOT NULL,
          [StockQuantity] int NOT NULL,
          [IsActive] bit NOT NULL,
          [CreatedAt] datetime2 NOT NULL,
          [UpdatedAt] datetime2 NULL,
          CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Users] (
          [Id] uniqueidentifier NOT NULL,
          [Email] nvarchar(255) NOT NULL,
          [PasswordHash] nvarchar(max) NOT NULL,
          [FirstName] nvarchar(100) NOT NULL,
          [LastName] nvarchar(100) NOT NULL,
          [PhoneNumber] nvarchar(20) NULL,
          [UserType] int NOT NULL,
          [IsActive] bit NOT NULL,
          [CreatedAt] datetime2 NOT NULL,
          [UpdatedAt] datetime2 NULL,
          [LastLoginAt] datetime2 NULL,
          CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (7ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Orders] (
          [Id] uniqueidentifier NOT NULL,
          [OrderNumber] nvarchar(50) NOT NULL,
          [CustomerId] uniqueidentifier NOT NULL,
          [OrderDate] datetime2 NOT NULL,
          [SubTotal] decimal(18,2) NOT NULL,
          [TaxAmount] decimal(18,2) NOT NULL,
          [ShippingCost] decimal(18,2) NOT NULL,
          [DiscountAmount] decimal(18,2) NOT NULL,
          [TotalAmount] decimal(18,2) NOT NULL,
          [OrderStatus] nvarchar(50) NOT NULL,
          [PaymentStatus] nvarchar(50) NOT NULL,
          [ProcessedBy] uniqueidentifier NULL,
          [ShippedDate] datetime2 NULL,
          [DeliveredDate] datetime2 NULL,
          [CancelledDate] datetime2 NULL,
          [CreatedAt] datetime2 NOT NULL,
          [UpdatedAt] datetime2 NULL,
          CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Orders_Users_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
          CONSTRAINT [FK_Orders_Users_ProcessedBy] FOREIGN KEY ([ProcessedBy]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (6ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [OrderItems] (
          [Id] int NOT NULL IDENTITY,
          [OrderId] uniqueidentifier NOT NULL,
          [ProductId] int NOT NULL,
          [Quantity] int NOT NULL,
          [UnitPrice] decimal(18,2) NOT NULL,
          [TotalPrice] decimal(18,2) NOT NULL,
          [DiscountAmount] decimal(18,2) NOT NULL,
          [ProductSKU] nvarchar(50) NOT NULL,
          [ProductName] nvarchar(200) NOT NULL,
          CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
          CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE UNIQUE INDEX [IX_Orders_OrderNumber] ON [Orders] ([OrderNumber]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Orders_OrderStatus] ON [Orders] ([OrderStatus]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Orders_ProcessedBy] ON [Orders] ([ProcessedBy]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Products_Category] ON [Products] ([Category]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE UNIQUE INDEX [IX_Products_SKU] ON [Products] ([SKU]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (8ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20260727104758_InitialCreate', N'8.0.0');
Done.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT name FROM sys.databases WHERE name = 'AgenticCommerceDB'"
name
--------------------------------------------------------------------------------------------------------------------------------
AgenticCommerceDB

(1 rows affected)

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>sqlcmd -S (localdb)\mssqllocaldb -d AgenticCommerceDB -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'"
TABLE_NAME
--------------------------------------------------------------------------------------------------------------------------------
__EFMigrationsHistory
Products
Users
Orders
OrderItems

(5 rows affected)

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.AspNetCore.Authentication.JwtBearer' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/index.json 300ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/8.0.0/microsoft.aspnetcore.authentication.jwtbearer.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/8.0.0/microsoft.aspnetcore.authentication.jwtbearer.8.0.0.nupkg 325ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/7.0.3/microsoft.identitymodel.protocols.openidconnect.7.0.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/7.0.3/microsoft.identitymodel.protocols.openidconnect.7.0.3.nupkg 84ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/7.0.3/microsoft.identitymodel.protocols.7.0.3.nupkg
info :   CACHE https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/7.0.3/system.identitymodel.tokens.jwt.7.0.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/7.0.3/microsoft.identitymodel.protocols.7.0.3.nupkg 55ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/7.0.3/system.identitymodel.tokens.jwt.7.0.3.nupkg 241ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/index.json
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/7.0.3/microsoft.identitymodel.logging.7.0.3.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/7.0.3/microsoft.identitymodel.tokens.7.0.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/7.0.3/microsoft.identitymodel.logging.7.0.3.nupkg 59ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/7.0.3/microsoft.identitymodel.jsonwebtokens.7.0.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/7.0.3/microsoft.identitymodel.tokens.7.0.3.nupkg 359ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/7.0.3/microsoft.identitymodel.abstractions.7.0.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/7.0.3/microsoft.identitymodel.abstractions.7.0.3.nupkg 73ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/7.0.3/microsoft.identitymodel.jsonwebtokens.7.0.3.nupkg 197ms
info : Installed Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.aspnetcore.authentication.jwtbearer\8.0.0 with content hash rwxaZYHips5M9vqxRkGfJthTx+Ws4O4yCuefn17J371jL3ouC5Ker43h2hXb5yd9BMnImE9rznT75KJHm6bMgg==.
info : Installed System.IdentityModel.Tokens.Jwt 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.identitymodel.tokens.jwt\7.0.3 with content hash caEe+OpQNYNiyZb+DJpUVROXoVySWBahko2ooNfUcllxa9ZQUM8CgM/mDjP6AoFn6cQU9xMmG+jivXWub8cbGg==.
info : Installed Microsoft.IdentityModel.Protocols 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.protocols\7.0.3 with content hash BtwR+tctBYhPNygyZmt1Rnw74GFrJteW+1zcdIgyvBCjkek6cNwPPqRfdhzCv61i+lwyNomRi8+iI4QKd4YCKA==.
info : Installed Microsoft.IdentityModel.Protocols.OpenIdConnect 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.protocols.openidconnect\7.0.3 with content hash W97TraHApDNArLwpPcXfD+FZH7njJsfEwZE9y9BoofeXMS8H0LBBobz0VOmYmMK4mLdOKxzN7SFT3Ekg0FWI3Q==.
info : Installed Microsoft.IdentityModel.Logging 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.logging\7.0.3 with content hash b6GbGO+2LOTBEccHhqoJsOsmemG4A/MY+8H0wK/ewRhiG+DCYwEnucog1cSArPIY55zcn+XdZl0YEiUHkpDISQ==.
info : Installed Microsoft.IdentityModel.Abstractions 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.abstractions\7.0.3 with content hash cfPUWdjigLIRIJSKz3uaZxShgf86RVDXHC1VEEchj1gnY25akwPYpbrfSoIGDCqA9UmOMdlctq411+2pAViFow==.
info : Installed Microsoft.IdentityModel.JsonWebTokens 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.jsonwebtokens\7.0.3 with content hash vxjHVZbMKD3rVdbvKhzAW+7UiFrYToUVm3AGmYfKSOAwyhdLl/ELX1KZr+FaLyyS5VReIzWRWJfbOuHM9i6ywg==.
info : Installed Microsoft.IdentityModel.Tokens 7.0.3 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.tokens\7.0.3 with content hash wB+LlbDjhnJ98DULjmFepqf9eEMh/sDs6S6hFh68iNRHmwollwhxk+nbSSfpA5+j+FbRyNskoaY4JsY1iCOKCg==.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
info : Package 'Microsoft.AspNetCore.Authentication.JwtBearer' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'Microsoft.AspNetCore.Authentication.JwtBearer' version '8.0.0' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 3.99 sec).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package System.IdentityModel.Tokens.Jwt --version 7.0.0
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'System.IdentityModel.Tokens.Jwt' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   CACHE https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/7.0.0/system.identitymodel.tokens.jwt.7.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/7.0.0/system.identitymodel.tokens.jwt.7.0.0.nupkg 355ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/index.json
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/7.0.0/microsoft.identitymodel.tokens.7.0.0.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/7.0.0/microsoft.identitymodel.jsonwebtokens.7.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/7.0.0/microsoft.identitymodel.tokens.7.0.0.nupkg 17ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/7.0.0/microsoft.identitymodel.jsonwebtokens.7.0.0.nupkg 199ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/7.0.0/microsoft.identitymodel.logging.7.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/7.0.0/microsoft.identitymodel.logging.7.0.0.nupkg 48ms
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/7.0.0/microsoft.identitymodel.abstractions.7.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.abstractions/7.0.0/microsoft.identitymodel.abstractions.7.0.0.nupkg 43ms
info : Installed Microsoft.IdentityModel.Logging 7.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.logging\7.0.0 with content hash 6I35Kt2/PQZAyUYLo3+QgT/LubZ5/4Ojelkbyo8KKdDgjMbVocAx2B3P5V7iMCz+rsAe/RLr6ql87QKnHtI+aw==.
info : Installed Microsoft.IdentityModel.Abstractions 7.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.abstractions\7.0.0 with content hash 7iSWSRR72VKeonFdfDi43Lvkca98Y0F3TmmWhRSuHbkjk/IKUSO0Qd272LQFZpi5eDNQNbUXy3o4THXhOAU6cw==.
info : Installed System.IdentityModel.Tokens.Jwt 7.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.identitymodel.tokens.jwt\7.0.0 with content hash 3OpN2iJf8lxpzVeFeeZSLtR3co6uKBs3VudS3PkkgdX5WF9fqqdhRMYf7WbkxqWQP/9RpoFbD3RimhfJe3hlQQ==.
info : Installed Microsoft.IdentityModel.JsonWebTokens 7.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.jsonwebtokens\7.0.0 with content hash N+hUPsFZs+IhlMU+qmX8NnYVB9uMxVdcWoPIhKo4oHDR/yuIFh19SVZeFby15cm8S9yedynOcfs7TU5oDCheZw==.
info : Installed Microsoft.IdentityModel.Tokens 7.0.0 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\microsoft.identitymodel.tokens\7.0.0 with content hash dxYqmmFLsjBQZ6F6a4XDzrZ1CTxBRFVigJvWiNtXiIsT6UlYMxs9ONMaGx9XKzcxmcgEQ2ADuCqKZduz0LR9Hw==.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
warn : NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.0 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
error: NU1605: Warning As Error: Detected package downgrade: System.IdentityModel.Tokens.Jwt from 7.0.3 to 7.0.0. Reference the package directly from the project to select a different version.
error:  AI-Ecommerce.Api -> Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0 -> Microsoft.IdentityModel.Protocols.OpenIdConnect 7.0.3 -> System.IdentityModel.Tokens.Jwt (>= 7.0.3)
error:  AI-Ecommerce.Api -> System.IdentityModel.Tokens.Jwt (>= 7.0.0)
info : Package 'System.IdentityModel.Tokens.Jwt' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'System.IdentityModel.Tokens.Jwt' version '7.0.0' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Failed to restore C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 3.72 sec).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.0 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : error NU1605:
      Warning As Error: Detected package downgrade: System.IdentityModel.Tokens.Jwt from 7.0.3 to 7.0.0. Reference the package direc
      tly from the project to select a different version.
       AI-Ecommerce.Api -> Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0 -> Microsoft.IdentityModel.Protocols.OpenIdConnect 7.
      0.3 -> System.IdentityModel.Tokens.Jwt (>= 7.0.3)
       AI-Ecommerce.Api -> System.IdentityModel.Tokens.Jwt (>= 7.0.0)

The build failed. Fix the build errors and run again.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package System.IdentityModel.Tokens.Jwt --version 7.0.3
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'System.IdentityModel.Tokens.Jwt' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
warn : NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info : Package 'System.IdentityModel.Tokens.Jwt' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'System.IdentityModel.Tokens.Jwt' version '7.0.3' updated in file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 563 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package System.IdentityModel.Tokens.Jwt --version 7.0.3
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'System.IdentityModel.Tokens.Jwt' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.24.23.40.32/vulnerability.update.json
warn : NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info : Package 'System.IdentityModel.Tokens.Jwt' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'System.IdentityModel.Tokens.Jwt' version '7.0.3' updated in file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Assets file has not changed. Skipping assets file writing. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 503 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore succeeded with 1 warning(s) in 1.5s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build succeeded with 1 warning(s) in 1.7s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 1 warning(s) in 1.0s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Data net8.0 succeeded (3.6s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 succeeded (0.8s) → src\AI-Ecommerce.Agent\bin\Debug\net8.0\AI-Ecommerce.Agent.dll
  AI-Ecommerce.Api net8.0 succeeded with 3 warning(s) (3.0s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Services\JwtService.cs(19,71): warning CS8604: Possible null reference argument for parameter 's' in 'byte[] Encoding.GetBytes(string s)'.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Program.cs(35,40): warning CS8604: Possible null reference argument for parameter 's' in 'byte[] Encoding.GetBytes(string s)'.
  AI-Ecommerce.Tests net8.0 succeeded (1.6s) → tests\AI-Ecommerce.Tests\bin\Debug\net8.0\AI-Ecommerce.Tests.dll

Build succeeded with 4 warning(s) in 10.1s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
Restore succeeded with 1 warning(s) in 0.7s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Api net8.0 succeeded with 1 warning(s) (0.3s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (86ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[UserType] = 1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (52ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = DateTime2), @p2='?' (Size = 255), @p3='?' (Size = 100), @p4='?' (DbType = Boolean), @p5='?' (DbType = DateTime2), @p6='?' (Size = 100), @p7='?' (Size = 4000), @p8='?' (Size = 20), @p9='?' (DbType = DateTime2), @p10='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SET IMPLICIT_TRANSACTIONS OFF;
      SET NOCOUNT ON;
      INSERT INTO [Users] ([Id], [CreatedAt], [Email], [FirstName], [IsActive], [LastLoginAt], [LastName], [PasswordHash], [PhoneNumber], [UpdatedAt], [UserType])
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (11ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Products] AS [p]) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (27ms) [Parameters=[@p0='?' (Size = 100), @p1='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p2='?' (DbType = DateTime2), @p3='?' (Size = 4000), @p4='?' (DbType = Boolean), @p5='?' (Size = 200), @p6='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p7='?' (Size = 50), @p8='?' (DbType = Int32), @p9='?' (DbType = DateTime2), @p10='?' (Size = 100), @p11='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p12='?' (DbType = DateTime2), @p13='?' (Size = 4000), @p14='?' (DbType = Boolean), @p15='?' (Size = 200), @p16='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p17='?' (Size = 50), @p18='?' (DbType = Int32), @p19='?' (DbType = DateTime2), @p20='?' (Size = 100), @p21='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p22='?' (DbType = DateTime2), @p23='?' (Size = 4000), @p24='?' (DbType = Boolean), @p25='?' (Size = 200), @p26='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p27='?' (Size = 50), @p28='?' (DbType = Int32), @p29='?' (DbType = DateTime2)], CommandType='Text', CommandTimeout='30']
      SET IMPLICIT_TRANSACTIONS OFF;
      SET NOCOUNT ON;
      MERGE [Products] USING (
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, 0),
      (@p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, 1),
      (@p20, @p21, @p22, @p23, @p24, @p25, @p26, @p27, @p28, @p29, 2)) AS i ([Category], [Cost], [CreatedAt], [Description], [IsActive], [Name], [Price], [SKU], [StockQuantity], [UpdatedAt], _Position) ON 1=0
      WHEN NOT MATCHED THEN
      INSERT ([Category], [Cost], [CreatedAt], [Description], [IsActive], [Name], [Price], [SKU], [StockQuantity], [UpdatedAt])
      VALUES (i.[Category], i.[Cost], i.[CreatedAt], i.[Description], i.[IsActive], i.[Name], i.[Price], i.[SKU], i.[StockQuantity], i.[UpdatedAt])
      OUTPUT INSERTED.[Id], i._Position;
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5015
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
Restore succeeded with 1 warning(s) in 0.6s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Api net8.0 succeeded with 1 warning(s) (0.2s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (34ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[UserType] = 1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Products] AS [p]) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5015
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
Restore succeeded with 1 warning(s) in 1.3s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Api net8.0 succeeded with 1 warning(s) (0.5s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (38ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[UserType] = 1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (6ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Products] AS [p]) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5015
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (72ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[Email] = @__request_Email_0) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (25ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = DateTime2), @p2='?' (Size = 255), @p3='?' (Size = 100), @p4='?' (DbType = Boolean), @p5='?' (DbType = DateTime2), @p6='?' (Size = 100), @p7='?' (Size = 4000), @p8='?' (Size = 20), @p9='?' (DbType = DateTime2), @p10='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SET IMPLICIT_TRANSACTIONS OFF;
      SET NOCOUNT ON;
      INSERT INTO [Users] ([Id], [CreatedAt], [Email], [FirstName], [IsActive], [LastLoginAt], [LastName], [PasswordHash], [PhoneNumber], [UpdatedAt], [UserType])
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10);
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>sqlcmd -S (localdb)\mssqllocaldb -d AgenticCommerceDB -Q "SELECT Email, FirstName, LastName, UserType FROM Users"
Email                                                                                                                                                                                                                                                           FirstName                                                                                            LastName                                                                                             UserType
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- ---------------------------------------------------------------------------------------------------- ---------------------------------------------------------------------------------------------------- -----------
customer@example.com                                                                                                                                                                                                                                            John                                                                                                 Doe                                                                                                            4
masteradmin@example.com                                                                                                                                                                                                                                         Master                                                                                               Admin                                                                                                          1

(2 rows affected)

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
Restore succeeded with 1 warning(s) in 0.7s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Api net8.0 succeeded with 1 warning(s) (0.3s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (46ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[UserType] = 1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Products] AS [p]) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5015
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (38ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [u].[Id], [u].[CreatedAt], [u].[Email], [u].[FirstName], [u].[IsActive], [u].[LastLoginAt], [u].[LastName], [u].[PasswordHash], [u].[PhoneNumber], [u].[UpdatedAt], [u].[UserType]
      FROM [Users] AS [u]
      WHERE [u].[Email] = @__request_Email_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (17ms) [Parameters=[@p1='?' (DbType = Guid), @p0='?' (DbType = DateTime2)], CommandType='Text', CommandTimeout='30']
      SET IMPLICIT_TRANSACTIONS OFF;
      SET NOCOUNT ON;
      UPDATE [Users] SET [LastLoginAt] = @p0
      OUTPUT 1
      WHERE [Id] = @p1;
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet ef migrations add AddCustomerAndEmployee --project src/AI-Ecommerce.Data --startup-project src/AI-Ecommerce.Api
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
Build started...
Build failed. Use dotnet build to see the errors.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet ef database update --project src/AI-Ecommerce.Data --startup-project src/AI-Ecommerce.Api
C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
Build started...
Build failed. Use dotnet build to see the errors.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 1 warning(s) in 2.2s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Data net8.0 succeeded (0.7s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 succeeded (0.4s) → src\AI-Ecommerce.Agent\bin\Debug\net8.0\AI-Ecommerce.Agent.dll
  AI-Ecommerce.Api net8.0 failed with 5 error(s) and 1 warning(s) (5.8s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Controller\AgentController.cs(1,26): error CS0234: The type or namespace name 'Harness' does not exist in the namespace 'AI_Ecommerce.Agent' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Program.cs(7,26): error CS0234: The type or namespace name 'Harness' does not exist in the namespace 'AI_Ecommerce.Agent' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Program.cs(8,28): error CS0234: The type or namespace name 'AI' does not exist in the namespace 'Microsoft.Extensions' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Controller\AgentController.cs(13,22): error CS0246: The type or namespace name 'AgentHarness' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Controller\AgentController.cs(15,28): error CS0246: The type or namespace name 'AgentHarness' could not be found (are you missing a using directive or an assembly reference?)

Build failed with 5 error(s) and 2 warning(s) in 9.4s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>mkdir src\AI-Ecommerce.Agent\Harness

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore succeeded with 1 warning(s) in 1.8s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build succeeded with 1 warning(s) in 2.0s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 1 warning(s) in 0.9s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Data net8.0 succeeded (0.4s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 failed with 12 error(s) and 1 warning(s) (3.0s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(1,28): error CS0234: The type or namespace name 'AI' does not exist in the namespace 'Microsoft.Extensions' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(1,28): error CS0234: The type or namespace name 'AI' does not exist in the namespace 'Microsoft.Extensions' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0246: The type or namespace name 'IChatClient' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(9,26): error CS0246: The type or namespace name 'IChatClient' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(8,19): error CS0246: The type or namespace name 'ChatMessage' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(9,13): error CS0246: The type or namespace name 'ChatOptions' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(7,27): error CS0246: The type or namespace name 'ChatResponse' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(38,19): error CS0246: The type or namespace name 'ChatMessage' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(39,13): error CS0246: The type or namespace name 'ChatOptions' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,39): error CS0246: The type or namespace name 'ChatResponseUpdate' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(11,50): error CS0246: The type or namespace name 'ChatMessage' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(13,29): error CS0246: The type or namespace name 'IChatClient' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,59): warning CS8425: Async-iterator 'MockChatClient.GetStreamingResponseAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)' has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
  AI-Ecommerce.Api net8.0 failed with 1 warning(s) (0.1s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build failed with 12 error(s) and 3 warning(s) in 4.5s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
Restore succeeded with 1 warning(s) in 0.6s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Agent net8.0 failed with 12 error(s) and 1 warning(s) (0.3s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(1,28): error CS0234: The type or namespace name 'AI' does not exist in the namespace 'Microsoft.Extensions' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(1,28): error CS0234: The type or namespace name 'AI' does not exist in the namespace 'Microsoft.Extensions' (are you missing an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0246: The type or namespace name 'IChatClient' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(9,26): error CS0246: The type or namespace name 'IChatClient' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(11,50): error CS0246: The type or namespace name 'ChatMessage' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\AgentHarness.cs(13,29): error CS0246: The type or namespace name 'IChatClient' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(8,19): error CS0246: The type or namespace name 'ChatMessage' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(9,13): error CS0246: The type or namespace name 'ChatOptions' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(7,27): error CS0246: The type or namespace name 'ChatResponse' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(38,19): error CS0246: The type or namespace name 'ChatMessage' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(39,13): error CS0246: The type or namespace name 'ChatOptions' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,39): error CS0246: The type or namespace name 'ChatResponseUpdate' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,59): warning CS8425: Async-iterator 'MockChatClient.GetStreamingResponseAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)' has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
  AI-Ecommerce.Api net8.0 failed with 1 warning(s) (0.0s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

The build failed. Fix the build errors and run again.

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>cd src/AI-Ecommerce.Agent

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent>dotnet add package Microsoft.Extensions.AI --version 9.0.0-preview.9.24507.7
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.AI' into project 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.text.json/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/index.json 15ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.text.json/index.json 30ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.text.json/9.0.0-rc.2.24473.5/system.text.json.9.0.0-rc.2.24473.5.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/9.0.0-rc.2.24473.5/system.diagnostics.diagnosticsource.9.0.0-rc.2.24473.5.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.diagnosticsource/9.0.0-rc.2.24473.5/system.diagnostics.diagnosticsource.9.0.0-rc.2.24473.5.nupkg 22ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.text.json/9.0.0-rc.2.24473.5/system.text.json.9.0.0-rc.2.24473.5.nupkg 31ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.io.pipelines/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.text.encodings.web/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/system.io.pipelines/index.json 26ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.text.encodings.web/index.json 11ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.io.pipelines/9.0.0-rc.2.24473.5/system.io.pipelines.9.0.0-rc.2.24473.5.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/system.text.encodings.web/9.0.0-rc.2.24473.5/system.text.encodings.web.9.0.0-rc.2.24473.5.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.text.encodings.web/9.0.0-rc.2.24473.5/system.text.encodings.web.9.0.0-rc.2.24473.5.nupkg 16ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.io.pipelines/9.0.0-rc.2.24473.5/system.io.pipelines.9.0.0-rc.2.24473.5.nupkg 26ms
info : Installed System.IO.Pipelines 9.0.0-rc.2.24473.5 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.io.pipelines\9.0.0-rc.2.24473.5 with content hash imrG8NvYH2f4Pdiu9xDUh2X7yoJgl6ginhAzuozH3HKERVf8kOOG09QrAnmgMNHLiWKsTztmE0CyzqfQs/X1IA==.
info : Installed System.Text.Encodings.Web 9.0.0-rc.2.24473.5 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.text.encodings.web\9.0.0-rc.2.24473.5 with content hash nBIVPcHg6sqqiV7pWxDxldAaiHCSrAcUU84WfDkjZrrvowv8VzU55K1k7qPw76k2xMnxbfCYZv7gYX7QPuE0fw==.
info : Installed System.Diagnostics.DiagnosticSource 9.0.0-rc.2.24473.5 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.diagnostics.diagnosticsource\9.0.0-rc.2.24473.5 with content hash 1trPm7zM4c15wMJVMV67XbYhA96wIIuBsEunorYRSrgKbFBZeG6gEGwiKy53SFJBpzq6oy+2kYckPaBzsK5p8w==.
info : Installed System.Text.Json 9.0.0-rc.2.24473.5 from https://api.nuget.org/v3/index.json to C:\Users\Goldmedal\.nuget\packages\system.text.json\9.0.0-rc.2.24473.5 with content hash IsQCD+zBcFhteX7fUrS4cU/GvfLPy8F4oLtC9VBcF1U1qu1gZB/zlAxW8G0kqmAiXI84/gowZtcX1MjLk2QWoQ==.
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 220ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json 219ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json 218ms
info : Package 'Microsoft.Extensions.AI' is compatible with all the specified frameworks in project 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : PackageReference for package 'Microsoft.Extensions.AI' version '9.0.0-preview.9.24507.7' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\AI-Ecommerce.Agent.csproj.nuget.g.targets.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj (in 3.46 sec).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent>dotnet add package Microsoft.Extensions.AI.Abstractions --version 9.0.0-preview.9.24507.7
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.AI.Abstractions' into project 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj...
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
info : Package 'Microsoft.Extensions.AI.Abstractions' is compatible with all the specified frameworks in project 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : PackageReference for package 'Microsoft.Extensions.AI.Abstractions' version '9.0.0-preview.9.24507.7' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj (in 531 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent>cd ../..

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package Microsoft.Extensions.AI --version 9.0.0-preview.9.24507.7
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.AI' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
warn : NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info : Package 'Microsoft.Extensions.AI' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'Microsoft.Extensions.AI' version '9.0.0-preview.9.24507.7' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 523 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj package Microsoft.Extensions.AI.Abstractions --version 9.0.0-preview.9.24507.7
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.AI.Abstractions' into project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj...
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
warn : NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info : Package 'Microsoft.Extensions.AI.Abstractions' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj'.
info : PackageReference for package 'Microsoft.Extensions.AI.Abstractions' version '9.0.0-preview.9.24507.7' added to file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj (in 486 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet clean

Build succeeded in 2.1s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>rmdir /s /q src\AI-Ecommerce.Agent\bin 2>nul

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>rmdir /s /q src\AI-Ecommerce.Agent\obj 2>nul

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>rmdir /s /q src\AI-Ecommerce.Api\bin 2>nul

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>rmdir /s /q src\AI-Ecommerce.Api\obj 2>nul

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : error NU1605:
      Warning As Error: Detected package downgrade: Microsoft.Extensions.Logging.Abstractions from 9.0.0-rc.2.24473.5 to 8.0.0. Refe
      rence the package directly from the project to select a different version.
       AI-Ecommerce.Agent -> Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 -> Microsoft.Extensions.Logging.Abstractions (>= 9.0.0-
      rc.2.24473.5)
       AI-Ecommerce.Agent -> Microsoft.Extensions.Logging.Abstractions (>= 8.0.0)

Restore failed with 1 error(s) and 1 warning(s) in 2.1s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : error NU1605:
      Warning As Error: Detected package downgrade: Microsoft.Extensions.Logging.Abstractions from 9.0.0-rc.2.24473.5 to 8.0.0. Refe
      rence the package directly from the project to select a different version.
       AI-Ecommerce.Agent -> Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 -> Microsoft.Extensions.Logging.Abstractions (>= 9.0.0-
      rc.2.24473.5)
       AI-Ecommerce.Agent -> Microsoft.Extensions.Logging.Abstractions (>= 8.0.0)

Restore failed with 1 error(s) and 1 warning(s) in 1.7s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj package Microsoft.Extensions.Logging.Abstractions --version 9.0.0-rc.2.24473.5
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.Logging.Abstractions' into project 'src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj...
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
info : Package 'Microsoft.Extensions.Logging.Abstractions' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj'.
info : PackageReference for package 'Microsoft.Extensions.Logging.Abstractions' version '9.0.0-rc.2.24473.5' updated in file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Generating MSBuild file C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\AI-Ecommerce.Agent.csproj.nuget.g.targets.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj (in 668 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore succeeded with 1 warning(s) in 1.1s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build succeeded with 1 warning(s) in 1.4s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 1 warning(s) in 1.2s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Data net8.0 succeeded (2.2s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 failed with 6 error(s) and 1 warning(s) (0.7s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(7,27): error CS0246: The type or namespace name 'ChatResponse' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,39): error CS0246: The type or namespace name 'ChatResponseUpdate' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.CompleteAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.CompleteStreamingAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.GetService<TService>(object?)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.Metadata'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,59): warning CS8425: Async-iterator 'MockChatClient.GetStreamingResponseAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)' has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
  AI-Ecommerce.Api net8.0 failed with 1 warning(s) (0.1s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build failed with 6 error(s) and 3 warning(s) in 4.4s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet clean

Build succeeded in 1.2s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore succeeded with 1 warning(s) in 1.4s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build succeeded with 1 warning(s) in 1.7s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 1 warning(s) in 0.9s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
  AI-Ecommerce.Data net8.0 succeeded (1.1s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 failed with 6 error(s) and 1 warning(s) (0.6s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(7,27): error CS0246: The type or namespace name 'ChatResponse' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,39): error CS0246: The type or namespace name 'ChatResponseUpdate' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.CompleteAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.CompleteStreamingAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.GetService<TService>(object?)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.Metadata'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,59): warning CS8425: Async-iterator 'MockChatClient.GetStreamingResponseAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)' has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
  AI-Ecommerce.Api net8.0 failed with 1 warning(s) (0.1s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build failed with 6 error(s) and 3 warning(s) in 2.7s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj package Microsoft.Extensions.AI --version 9.0.0-preview.6.24333.2
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.AI' into project 'src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.ai/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.ai/index.json 228ms
warn : NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
info : Package 'Microsoft.Extensions.AI' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj'.
info : PackageReference for package 'Microsoft.Extensions.AI' version '9.0.0-preview.6.24333.2' updated in file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj (in 1 sec).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet add src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj package Microsoft.Extensions.AI.Abstractions --version 9.0.0-preview.6.24333.2
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'Microsoft.Extensions.AI.Abstractions' into project 'src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj'.
info : Restoring packages for C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj...
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.extensions.ai/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.ai.abstractions/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.ai.abstractions/index.json 153ms
warn : NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
warn : NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2026.07.24.23.40.32/2026.07.27.23.40.38/vulnerability.update.json
info : Package 'Microsoft.Extensions.AI.Abstractions' is compatible with all the specified frameworks in project 'src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj'.
info : PackageReference for package 'Microsoft.Extensions.AI.Abstractions' version '9.0.0-preview.6.24333.2' updated in file 'C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj'.
info : Writing assets file to disk. Path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\obj\project.assets.json
log  : Restored C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj (in 598 ms).

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet clean

Build succeeded in 0.9s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore succeeded with 3 warning(s) in 0.9s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.

Build succeeded with 3 warning(s) in 1.1s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 3 warning(s) in 0.9s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
  AI-Ecommerce.Data net8.0 succeeded (0.5s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 failed with 6 error(s) and 3 warning(s) (0.6s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(7,27): error CS0246: The type or namespace name 'ChatResponse' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,39): error CS0246: The type or namespace name 'ChatResponseUpdate' could not be found (are you missing a using directive or an assembly reference?)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.CompleteAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.CompleteStreamingAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.GetService<TService>(object?)'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(5,35): error CS0535: 'MockChatClient' does not implement interface member 'IChatClient.Metadata'
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\Harness\MockChatClient.cs(37,59): warning CS8425: Async-iterator 'MockChatClient.GetStreamingResponseAsync(IList<ChatMessage>, ChatOptions?, CancellationToken)' has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
  AI-Ecommerce.Api net8.0 failed with 1 warning(s) (0.1s)
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52

Build failed with 6 error(s) and 7 warning(s) in 2.1s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet clean

Build succeeded in 1.3s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet restore
Restore succeeded with 3 warning(s) in 1.2s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.

Build succeeded with 3 warning(s) in 1.5s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet build
Restore succeeded with 3 warning(s) in 1.3s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
  AI-Ecommerce.Data net8.0 succeeded (0.6s) → src\AI-Ecommerce.Data\bin\Debug\net8.0\AI-Ecommerce.Data.dll
  AI-Ecommerce.Agent net8.0 succeeded with 2 warning(s) (1.1s) → src\AI-Ecommerce.Agent\bin\Debug\net8.0\AI-Ecommerce.Agent.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
  AI-Ecommerce.Api net8.0 succeeded with 5 warning(s) (3.1s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Services\JwtService.cs(19,71): warning CS8604: Possible null reference argument for parameter 's' in 'byte[] Encoding.GetBytes(string s)'.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Program.cs(37,40): warning CS8604: Possible null reference argument for parameter 's' in 'byte[] Encoding.GetBytes(string s)'.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Controller\OrdersController.cs(70,29): warning CS8602: Dereference of a possibly null reference.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\Controller\OrdersController.cs(118,27): warning CS8604: Possible null reference argument for parameter 'input' in 'Guid Guid.Parse(string input)'.
  AI-Ecommerce.Tests net8.0 succeeded (2.1s) → tests\AI-Ecommerce.Tests\bin\Debug\net8.0\AI-Ecommerce.Tests.dll

Build succeeded with 10 warning(s) in 8.3s

C:\Users\Goldmedal\Desktop\AgenticCommercePlatform>dotnet run --project src/AI-Ecommerce.Api
Using launch settings from src\AI-Ecommerce.Api\Properties\launchSettings.json...
Building...
Restore succeeded with 3 warning(s) in 1.1s
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
  AI-Ecommerce.Agent net8.0 succeeded with 2 warning(s) (0.3s) → src\AI-Ecommerce.Agent\bin\Debug\net8.0\AI-Ecommerce.Agent.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI 9.0.0-preview.9.24507.7 was resolved instead.
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Agent\AI-Ecommerce.Agent.csproj : warning NU1603: AI-Ecommerce.Agent depends on Microsoft.Extensions.AI.Abstractions (>= 9.0.0-preview.6.24333.2) but Microsoft.Extensions.AI.Abstractions 9.0.0-preview.6.24333.2 was not found. Microsoft.Extensions.AI.Abstractions 9.0.0-preview.9.24507.7 was resolved instead.
  AI-Ecommerce.Api net8.0 succeeded with 1 warning(s) (0.5s) → src\AI-Ecommerce.Api\bin\Debug\net8.0\AI-Ecommerce.Api.dll
    C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api\AI-Ecommerce.Api.csproj : warning NU1902: Package 'System.IdentityModel.Tokens.Jwt' 7.0.3 has a known moderate severity vulnerability, https://github.com/advisories/GHSA-59j7-ghrg-fj52
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (75ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[UserType] = 1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (8ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Products] AS [p]) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5015
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\Goldmedal\Desktop\AgenticCommercePlatform\src\AI-Ecommerce.Api
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (42ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [u].[Id], [u].[CreatedAt], [u].[Email], [u].[FirstName], [u].[IsActive], [u].[LastLoginAt], [u].[LastName], [u].[PasswordHash], [u].[PhoneNumber], [u].[UpdatedAt], [u].[UserType]
      FROM [Users] AS [u]
      WHERE [u].[Email] = @__request_Email_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [u].[Id], [u].[CreatedAt], [u].[Email], [u].[FirstName], [u].[IsActive], [u].[LastLoginAt], [u].[LastName], [u].[PasswordHash], [u].[PhoneNumber], [u].[UpdatedAt], [u].[UserType]
      FROM [Users] AS [u]
      WHERE [u].[Email] = @__request_Email_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[Email] = @__request_Email_0) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Users] AS [u]
              WHERE [u].[Email] = @__request_Email_0) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (27ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = DateTime2), @p2='?' (Size = 255), @p3='?' (Size = 100), @p4='?' (DbType = Boolean), @p5='?' (DbType = DateTime2), @p6='?' (Size = 100), @p7='?' (Size = 4000), @p8='?' (Size = 20), @p9='?' (DbType = DateTime2), @p10='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SET IMPLICIT_TRANSACTIONS OFF;
      SET NOCOUNT ON;
      INSERT INTO [Users] ([Id], [CreatedAt], [Email], [FirstName], [IsActive], [LastLoginAt], [LastName], [PasswordHash], [PhoneNumber], [UpdatedAt], [UserType])
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[@__request_Email_0='?' (Size = 255)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [u].[Id], [u].[CreatedAt], [u].[Email], [u].[FirstName], [u].[IsActive], [u].[LastLoginAt], [u].[LastName], [u].[PasswordHash], [u].[PhoneNumber], [u].[UpdatedAt], [u].[UserType]
      FROM [Users] AS [u]
      WHERE [u].[Email] = @__request_Email_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[@p1='?' (DbType = Guid), @p0='?' (DbType = DateTime2)], CommandType='Text', CommandTimeout='30']
      SET IMPLICIT_TRANSACTIONS OFF;
      SET NOCOUNT ON;
      UPDATE [Users] SET [LastLoginAt] = @p0
      OUTPUT 1
      WHERE [Id] = @p1;
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (11ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (23ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
      WHERE [p].[Id] = @__p_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
      WHERE [p].[Id] = @__p_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (104ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = DateTime2), @p2='?' (DbType = DateTime2), @p3='?' (DbType = Guid), @p4='?' (DbType = DateTime2), @p5='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p6='?' (DbType = DateTime2), @p7='?' (Size = 50), @p8='?' (Size = 50), @p9='?' (Size = 50), @p10='?' (DbType = Guid), @p11='?' (DbType = DateTime2), @p12='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p13='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p14='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p15='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p16='?' (DbType = DateTime2), @p18='?' (DbType = Int32), @p17='?' (DbType = Int32), @p20='?' (DbType = Int32), @p19='?' (DbType = Int32), @p21='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p22='?' (DbType = Guid), @p23='?' (DbType = Int32), @p24='?' (Size = 200), @p25='?' (Size = 50), @p26='?' (DbType = Int32), @p27='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p28='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p29='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p30='?' (DbType = Guid), @p31='?' (DbType = Int32), @p32='?' (Size = 200), @p33='?' (Size = 50), @p34='?' (DbType = Int32), @p35='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p36='?' (Precision = 18) (Scale = 2) (DbType = Decimal)], CommandType='Text', CommandTimeout='30']
      SET NOCOUNT ON;
      INSERT INTO [Orders] ([Id], [CancelledDate], [CreatedAt], [CustomerId], [DeliveredDate], [DiscountAmount], [OrderDate], [OrderNumber], [OrderStatus], [PaymentStatus], [ProcessedBy], [ShippedDate], [ShippingCost], [SubTotal], [TaxAmount], [TotalAmount], [UpdatedAt])
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16);
      UPDATE [Products] SET [StockQuantity] = @p17
      OUTPUT 1
      WHERE [Id] = @p18;
      UPDATE [Products] SET [StockQuantity] = @p19
      OUTPUT 1
      WHERE [Id] = @p20;
      MERGE [OrderItems] USING (
      VALUES (@p21, @p22, @p23, @p24, @p25, @p26, @p27, @p28, 0),
      (@p29, @p30, @p31, @p32, @p33, @p34, @p35, @p36, 1)) AS i ([DiscountAmount], [OrderId], [ProductId], [ProductName], [ProductSKU], [Quantity], [TotalPrice], [UnitPrice], _Position) ON 1=0
      WHEN NOT MATCHED THEN
      INSERT ([DiscountAmount], [OrderId], [ProductId], [ProductName], [ProductSKU], [Quantity], [TotalPrice], [UnitPrice])
      VALUES (i.[DiscountAmount], i.[OrderId], i.[ProductId], i.[ProductName], i.[ProductSKU], i.[Quantity], i.[TotalPrice], i.[UnitPrice])
      OUTPUT INSERTED.[Id], i._Position;
fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
      System.Text.Json.JsonException: A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32. Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles. Path: $.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.
         at System.Text.Json.ThrowHelper.ThrowJsonException_SerializerCycleDetected(Int32 maxDepth)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.WriteCore(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResultFilterAsync>g__Awaited|30_0[TFilter,TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
      --- End of stack trace from previous location ---
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
         at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
         at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
         at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
         at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (22ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
      WHERE [p].[Id] = @__p_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
      WHERE [p].[Id] = @__p_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (54ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = DateTime2), @p2='?' (DbType = DateTime2), @p3='?' (DbType = Guid), @p4='?' (DbType = DateTime2), @p5='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p6='?' (DbType = DateTime2), @p7='?' (Size = 50), @p8='?' (Size = 50), @p9='?' (Size = 50), @p10='?' (DbType = Guid), @p11='?' (DbType = DateTime2), @p12='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p13='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p14='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p15='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p16='?' (DbType = DateTime2), @p18='?' (DbType = Int32), @p17='?' (DbType = Int32), @p20='?' (DbType = Int32), @p19='?' (DbType = Int32), @p21='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p22='?' (DbType = Guid), @p23='?' (DbType = Int32), @p24='?' (Size = 200), @p25='?' (Size = 50), @p26='?' (DbType = Int32), @p27='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p28='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p29='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p30='?' (DbType = Guid), @p31='?' (DbType = Int32), @p32='?' (Size = 200), @p33='?' (Size = 50), @p34='?' (DbType = Int32), @p35='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p36='?' (Precision = 18) (Scale = 2) (DbType = Decimal)], CommandType='Text', CommandTimeout='30']
      SET NOCOUNT ON;
      INSERT INTO [Orders] ([Id], [CancelledDate], [CreatedAt], [CustomerId], [DeliveredDate], [DiscountAmount], [OrderDate], [OrderNumber], [OrderStatus], [PaymentStatus], [ProcessedBy], [ShippedDate], [ShippingCost], [SubTotal], [TaxAmount], [TotalAmount], [UpdatedAt])
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16);
      UPDATE [Products] SET [StockQuantity] = @p17
      OUTPUT 1
      WHERE [Id] = @p18;
      UPDATE [Products] SET [StockQuantity] = @p19
      OUTPUT 1
      WHERE [Id] = @p20;
      MERGE [OrderItems] USING (
      VALUES (@p21, @p22, @p23, @p24, @p25, @p26, @p27, @p28, 0),
      (@p29, @p30, @p31, @p32, @p33, @p34, @p35, @p36, 1)) AS i ([DiscountAmount], [OrderId], [ProductId], [ProductName], [ProductSKU], [Quantity], [TotalPrice], [UnitPrice], _Position) ON 1=0
      WHEN NOT MATCHED THEN
      INSERT ([DiscountAmount], [OrderId], [ProductId], [ProductName], [ProductSKU], [Quantity], [TotalPrice], [UnitPrice])
      VALUES (i.[DiscountAmount], i.[OrderId], i.[ProductId], i.[ProductName], i.[ProductSKU], i.[Quantity], i.[TotalPrice], i.[UnitPrice])
      OUTPUT INSERTED.[Id], i._Position;
fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
      System.Text.Json.JsonException: A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32. Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles. Path: $.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.
         at System.Text.Json.ThrowHelper.ThrowJsonException_SerializerCycleDetected(Int32 maxDepth)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.WriteCore(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResultFilterAsync>g__Awaited|30_0[TFilter,TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
      --- End of stack trace from previous location ---
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
         at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
         at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
         at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
         at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (7ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
      WHERE [p].[Id] = @__p_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [p].[Id], [p].[Category], [p].[Cost], [p].[CreatedAt], [p].[Description], [p].[IsActive], [p].[Name], [p].[Price], [p].[SKU], [p].[StockQuantity], [p].[UpdatedAt]
      FROM [Products] AS [p]
      WHERE [p].[Id] = @__p_0
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (39ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = DateTime2), @p2='?' (DbType = DateTime2), @p3='?' (DbType = Guid), @p4='?' (DbType = DateTime2), @p5='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p6='?' (DbType = DateTime2), @p7='?' (Size = 50), @p8='?' (Size = 50), @p9='?' (Size = 50), @p10='?' (DbType = Guid), @p11='?' (DbType = DateTime2), @p12='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p13='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p14='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p15='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p16='?' (DbType = DateTime2), @p18='?' (DbType = Int32), @p17='?' (DbType = Int32), @p20='?' (DbType = Int32), @p19='?' (DbType = Int32), @p21='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p22='?' (DbType = Guid), @p23='?' (DbType = Int32), @p24='?' (Size = 200), @p25='?' (Size = 50), @p26='?' (DbType = Int32), @p27='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p28='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p29='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p30='?' (DbType = Guid), @p31='?' (DbType = Int32), @p32='?' (Size = 200), @p33='?' (Size = 50), @p34='?' (DbType = Int32), @p35='?' (Precision = 18) (Scale = 2) (DbType = Decimal), @p36='?' (Precision = 18) (Scale = 2) (DbType = Decimal)], CommandType='Text', CommandTimeout='30']
      SET NOCOUNT ON;
      INSERT INTO [Orders] ([Id], [CancelledDate], [CreatedAt], [CustomerId], [DeliveredDate], [DiscountAmount], [OrderDate], [OrderNumber], [OrderStatus], [PaymentStatus], [ProcessedBy], [ShippedDate], [ShippingCost], [SubTotal], [TaxAmount], [TotalAmount], [UpdatedAt])
      VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16);
      UPDATE [Products] SET [StockQuantity] = @p17
      OUTPUT 1
      WHERE [Id] = @p18;
      UPDATE [Products] SET [StockQuantity] = @p19
      OUTPUT 1
      WHERE [Id] = @p20;
      MERGE [OrderItems] USING (
      VALUES (@p21, @p22, @p23, @p24, @p25, @p26, @p27, @p28, 0),
      (@p29, @p30, @p31, @p32, @p33, @p34, @p35, @p36, 1)) AS i ([DiscountAmount], [OrderId], [ProductId], [ProductName], [ProductSKU], [Quantity], [TotalPrice], [UnitPrice], _Position) ON 1=0
      WHEN NOT MATCHED THEN
      INSERT ([DiscountAmount], [OrderId], [ProductId], [ProductName], [ProductSKU], [Quantity], [TotalPrice], [UnitPrice])
      VALUES (i.[DiscountAmount], i.[OrderId], i.[ProductId], i.[ProductName], i.[ProductSKU], i.[Quantity], i.[TotalPrice], i.[UnitPrice])
      OUTPUT INSERTED.[Id], i._Position;
fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
      System.Text.Json.JsonException: A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32. Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles. Path: $.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.Order.OrderItems.
         at System.Text.Json.ThrowHelper.ThrowJsonException_SerializerCycleDetected(Int32 maxDepth)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Converters.ListOfTConverter`2.OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonCollectionConverter`2.OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonPropertyInfo`1.GetMemberAndWriteJson(Object obj, WriteStack& state, Utf8JsonWriter writer)
         at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.TryWrite(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.JsonConverter`1.WriteCore(Utf8JsonWriter writer, T& value, JsonSerializerOptions options, WriteStack& state)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.SerializeAsync(PipeWriter pipeWriter, T rootValue, Int32 flushThreshold, CancellationToken cancellationToken, Object rootValueBoxed)
         at Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResultFilterAsync>g__Awaited|30_0[TFilter,TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
      --- End of stack trace from previous location ---
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
         at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
         at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
         at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
         at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
