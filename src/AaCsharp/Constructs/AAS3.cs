using Amazon.CDK;

namespace AaCsharp.Constructs
{
    public class AAS3 : Construct
    {
        public AAS3(Construct scope, string nameId) : base(scope, nameId) 
        {
            var bucket = new Bucket(this, nameId, new BucketProps
            {
                BucketName = nameId,
                AutoDeleteObjects = true,

            })

        }
    }
}