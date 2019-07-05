# Optimg

Image optimization application which relies on [Kraken.io](Kraken.io) and can
be easily deployed to AWS lambda, relying in CloudFormation.

## Minimum requirements

* .NET Core 2.1 or above :)
* An AWS Account, and a lambda configured
* A Kraken.io account

## Getting started

Make sure you populate `src/appsettings.json` with the appropriate values.

If you don't have a bucket to store the function, create one

```
    aws s3 mb s3://BUCKET_NAME
```

### With sam CLI

1. Install sam CLI, using pip (also available via homebrew)
```
    pip install --user aws-sam-cli 
```

2. Package and deploy application
```
    sam package \
      --template-file template.yml \
      --output-template-file package.yml \
      --s3-bucket BUCKET_NAME
      
    sam deploy \
      --template-file package.yml \
      --stack-name sam-hello-world-1 \
      --capabilities CAPABILITY_IA
```

3. Invoke lambda locally
```
    sam build
    sam local invoke "HelloWorldFunction" -e ./test/testS3Event.json
```

### With dotnet CLI

1. Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

2. Deploy application
```
    dotnet lambda deploy-function OptimgFunction --project-location src/Optimg
```

3. To run the tests
```
    dotnet test test/Optimg.Tests
```

4. To execute the lambda:
```
    dotnet lambda invoke-function MyFunction --payload test/testS3Event.json
```

For more information on the lambda CLI tools please see: https://github.com/aws/aws-extensions-for-dotnet-cli

## Additional information

The main project consists of:

* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS
* serverless.template - an AWS CloudFormation Serverless Application Model template file for declaring your Serverless functions and other AWS resources
* Function.cs - class file containing the C# method mapped to the single function declared in the template file

