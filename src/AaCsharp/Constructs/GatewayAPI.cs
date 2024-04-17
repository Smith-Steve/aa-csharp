using Amazon.CDK.AWS.APIGateway;
using Constructs;

namespace AaCsharp.Constructs
{
	public class GatewayAPI : Construct
	{
		public GatewayAPI(Construct scope, string nameId, AALambda lambdaClass ) : base(scope, nameId) 
		{
			new LambdaRestApi(this, nameId, new LambdaRestApiProps
			{
				//This is where we define our rest API.
				//The Lambda class is passed in from the 'program' file.
				Handler = lambdaClass.Handler
			});
		}
	}
}