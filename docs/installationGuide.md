# .Net Package Installation

## Web Service to transform printable
### Transform printable 

## Pre-requisites

- IIS server
- .Net v4 installed on server
- Download and install the "Server Hosting Installer" from [here](https://www.microsoft.com/net/download/dotnet-core/runtime-2.0.6)
- Do an iisreset from the command line or reboot

## Installation guide

- Copy the contents of the 2code/Transformer/build folder into a folder under wwwroot
- In IIS Admin, promote the folder to an application. The default app pool is fine.

### Rollback steps

- Remove the build from the wwwroot folder
- Do an iisreset
