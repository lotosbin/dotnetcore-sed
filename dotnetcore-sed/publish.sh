#!/usr/bin/env bash

#!/bin/sh
dotnet publish -r osx-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true
dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true
dotnet publish -r linux-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true

mkdir ../dist
mkdir ../dist/osx
cp -r ./bin/Debug/net5.0/osx-x64/publish/* ../dist/osx/

mkdir ../dist/linux
cp -r ./bin/Debug/net5.0/linux-x64/publish/* ../dist/linux/

mkdir ../dist/win
cp -r ./bin/Debug/net5.0/win-x64/publish/* ../dist/win/

zip ../dist.zip ../dist/*