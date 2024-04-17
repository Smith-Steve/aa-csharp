using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Constructs;
using AaCsharp.Constructs;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace AaCsharp
{
    public class AaCsharpStack : Stack
    {

        internal AaCsharpStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
            AADynamoDBTableCreator dynamoTable = new AADynamoDBTableCreator(this, "HitTableAA");
            AALambda passedLambdaClass = new AALambda(this, "AALambda", dynamoTable);
            new GatewayAPI(this, "MyFirstAPI", passedLambdaClass);
        }
    }
}
