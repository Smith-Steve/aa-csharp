using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Constructs;
using System.Collections.Generic;

namespace AaCsharp.Constructs
{
    public class AADynamoDBTableCreator : Construct
    {
        public AADynamoDBTableCreator(Construct scope, string id) : base(scope, id)
        {
            var table = new TableV2(this, id, new TablePropsV2
            {
                PartitionKey = new Attribute
                {
                    Name = "path",
                    Type = AttributeType.STRING
                },
                RemovalPolicy = RemovalPolicy.DESTROY
            });
        }
    }
}