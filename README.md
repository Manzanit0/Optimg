# Optimg

Image optimization application which relies on [Kraken.io](Kraken.io) and can
be easily deployed to AWS lambda, relying in Terraform.

## Minimum requirements

* .NET Core 2.1 or above
* An AWS Account
* A Kraken.io account

## Getting started

Make sure you populate `src/appsettings.json` with the appropriate values.

The project uses Terraform for automating the creation of the required infrastructure
and dotnet CLI for testing the lambda remotely. You can also use the SAM CLI if you prefer,
but I've sticked with the dotnet for simplicity.

### Deploying the code

With [Terraform](https://www.terraform.io/), deploying the code is very simple, you just
have to execute `terraform apply`. Nonetheless, there are some gotchas for this specific project:

- You need to [**set some variables**](https://www.terraform.io/docs/configuration/variables.html),
either as environment variables, or via a `*.tfvars` file. They include the AWS region where you
want to create the infrastructure and the S3 Bucket which Optimg will be watching for events.

- You need to **have a bucket already created** which will trigger the events which Optimg will act
upon. The reason why I didn't automate the creation is because sometimes folks will want to add
Optimg to an already live project which already have a bunch of assets in an existing S3 Bucket.
This way, the project is good to go for them too.

- Also, last but not least, make sure you have your `~/.aws/credentials` set up with a default
profile. If you don't want to set one, feel free to modify the `terraform.tf` file to use whichever
profile you wish.

Once both conditions are met, `terraform apply` away!

For convenience, I have bootstraped a `deploy.sh` script in `scripts/` which builds the project
before running Terraform and also deploys the newest version of the code. To run it:

```
./scripts/deploy.sh lambda_name
```

### Testing the code

To run all the unit tests in the suite, run:

```
dotnet test test/Optimg.Tests
```

#### Using Lambda Tools CLI

For testing the lambda once it's already up and running in AWS, I've gone with the Lambda Tools.
At the end of the day, it's just a wrapper around the SAM CLI, but I've just found it overall
more comfortable to work with.

To install Amazon.Lambda.Tools Global Tools if not already installed.

```
dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.

```
dotnet tool update -g Amazon.Lambda.Tools
```

To execute the lambda once deployed:

```
dotnet lambda invoke-function LambdaName --payload test/testS3Event.json --region <aws_region>
```

For more information on the lambda CLI tools please see: https://github.com/aws/aws-extensions-for-dotnet-cli
