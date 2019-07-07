#!/usr/bin/env bash
dotnet build src/Optimg -c Release
terraform apply
dotnet lambda deploy-function $1 --project-location src/Optimg
