#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 9.0 -InstallDir ./dotnet
./dotnet/dotnet --version
./dotnet/dotnet run --project ./src/cv.PdfGenerator/cv.PdfGenerator.csproj --configuration Release
./dotnet/dotnet publish ./src/cv -c Release -o ./publish