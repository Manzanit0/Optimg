# Optimg

Image optimization application which relies on [Kraken.io](Kraken.io) and can
be easily deployed to AWS lambda, relying in CloudFormation.

## Minimum requirements

* .NET Core 2.1 or above :)
* An AWS Account, and a lambda configured
* A Kraken.io account

## Getting started

1. Make sure you populate `src/appsettings.json` with the appropiate values.

2. Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

3. Deploy application
```
    cd "Optimg/src/Optimg"
    dotnet lambda deploy-serverless
```

4. To run the tests
```
    cd "Optimg/test/Optimg.Tests/"
    dotnet test
```

The main project consists of:

* serverless.template - an AWS CloudFormation Serverless Application Model template file for declaring your Serverless functions and other AWS resources
* Function.cs - class file containing the C# method mapped to the single function declared in the template file
* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS

