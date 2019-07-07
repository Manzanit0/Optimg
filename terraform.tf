variable "aws_account_key" {
  type        = string
  default     = ""
}

variable "aws_account_secret" {
  type        = string
  default     = ""
}

variable "kraken_api_secret" {
  type        = string
  default     = ""
}

variable "kraken_api_key" {
  type        = string
  default     = ""
}

variable "region" {
  type        = string
  description = "AWS region to create the infrastructure in"
}

variable "watched_bucket_name" {
  type        = string
  description = "The name of the bucket which will trigger the lambda"
}

variable "watched_bucket_arn" {
  type        = string
  description = "The ARN of the bucket which will trigger the lambda"
}

provider "aws" {
  profile = "default"
  region  = var.region
}

data "archive_file" "lambda_zip" {
  type        = "zip"
  source_dir  = "src/Optimg/bin/Release/netcoreapp2.1/publish"
  output_path = "lambda.zip"
}

resource "aws_lambda_function" "optimg_lambda" {
  function_name = "optimg_lambda"
  handler       = "Optimg::Optimg.Function::FunctionHandler"
  runtime       = "dotnetcore2.1"
  timeout       = 30
  role          = aws_iam_role.lambda_exec_role.arn

  filename         = "lambda.zip"
  source_code_hash = data.archive_file.lambda_zip.output_base64sha256

  environment {
    variables = {
      Environment = terraform.workspace
      OPTIMG_AWS_REGION = var.region
      OPTIMG_AWS_ACCOUNT_KEY = var.aws_account_key
      OPTIMG_AWS_SECRET = var.aws_account_secret
      OPTIMG_AWS_S3_BUCKET = var.watched_bucket_name
      OPTIMG_KRAKEN_API_SECRET = var.kraken_api_secret
      OPTIMG_KRAKEN_API_KEY = var.kraken_api_key
    }
  }
}

resource "aws_lambda_permission" "allow_bucket" {
  statement_id  = "AllowExecutionFromS3Bucket"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.optimg_lambda.arn
  principal     = "s3.amazonaws.com"
  source_arn    = var.watched_bucket_arn
}

resource "aws_s3_bucket_notification" "bucket_notification" {
  bucket = var.watched_bucket_name

  lambda_function {
    lambda_function_arn = aws_lambda_function.optimg_lambda.arn
    events              = ["s3:ObjectCreated:*"]
    filter_suffix       = ".jpg"
  }
}

resource "aws_iam_role" "lambda_exec_role" {
  name               = "lambda_exec_role"
  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": "sts:AssumeRole",
      "Principal": {
        "Service": "lambda.amazonaws.com"
      },
      "Effect": "Allow",
      "Sid": ""
    }
  ]
}
EOF
}
