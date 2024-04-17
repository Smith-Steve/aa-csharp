using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Constructs;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.AccessControl;

namespace AaCsharp.Constructs
{
    public class AALambda : Construct
    {

        public Function Handler { get; }

        public AALambda(Construct scope, string nameId, AADynamoDBTableCreator table) : base(scope, nameId)
        {
            var secondLambdaId = nameId.Replace("AA", "AB");
            // We are creating an AWS Role to attach to the lambda.
            var iamRole = new Role(this, "NewIAMRole", new RoleProps
            {
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });


            iamRole.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole"));
            iamRole.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaDynamoDBExecutionRole"));
            iamRole.AttachInlinePolicy(new Policy(this, "AWSCDKDBLam", new PolicyProps
            {
                PolicyName = "ReadWriteDB",
                Statements = new [] {new PolicyStatement(new PolicyStatementProps
                {
                    Actions = new [] { "dynamodb:UpdateItem" }, 
                    Resources = new [] { "arn:aws:dynamodb:us-east-1:531698586584:table/AaCsharpStack-HitTableAA7718BE14-17CIFWJH1H0Z9" }
                }),
                new PolicyStatement(new PolicyStatementProps
                {
                    Actions = new [] { "lambda:Invoke", "lambda:InvokeFunction", "lambda:InvokeAsync" },
                    Resources = new [] { "arn:aws:lambda:us-east-1:531698586584:function:AaCsharpStack-AALambda5504FFDA-5rTfOblXF2vM" }
                })}
            }));
            
            Handler = new Function(this, nameId, new FunctionProps
            {
                Runtime = Runtime.NODEJS_16_X,
                Code = Code.FromAsset("lambda"),
                Handler = "hello.handler",
                Role = iamRole,
                Environment = new Dictionary<string, string>
                {
                    ["DOWNSTREAM_FUNCTION_NAME"] = "AaCsharpStack-AALambda5504FFDA-5rTfOblXF2vM",
                    ["HITS_TABLE_NAME"] = "AaCsharpStack-HitTableAA7718BE14-17CIFWJH1H0Z9"
                }
            });
        }
    }
}