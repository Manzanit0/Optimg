#!/usr/bin/env bash
dotnet build src/Optimg -c Release
terraform apply
