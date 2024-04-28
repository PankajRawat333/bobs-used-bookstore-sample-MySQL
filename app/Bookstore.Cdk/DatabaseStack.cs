namespace Bookstore.Cdk;

using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SSM;

using Constructs;

public class DatabaseStackProps : StackProps
{
    public Vpc Vpc { get; set; }
}

public class DatabaseStack : Stack
{
    private const int DatabasePort = 1433;

    public DatabaseInstance Database { get; set; }

    internal DatabaseStack(Construct scope, string id, DatabaseStackProps props) : base(scope, id, props)
    {
        var dbSG = new SecurityGroup(this, "DatabaseSecurityGroup", new SecurityGroupProps
        {
            Vpc = props.Vpc,
            Description = "Allow access to the MySQL instance from the website",
        });

        this.Database = new DatabaseInstance(this, $"{Constants.AppName}MySqlDb", new DatabaseInstanceProps
        {
            Vpc = props.Vpc,
            VpcSubnets = new SubnetSelection
            {
                SubnetType = SubnetType.PRIVATE_WITH_EGRESS
            },
            Engine = DatabaseInstanceEngine.Mysql(new MySqlInstanceEngineProps
            {
                Version = MysqlEngineVersion.VER_8_0_32 // Change the MySQL version to a supported one
            }),
            StorageType = StorageType.GP3,
            AllocatedStorage = 20,
            Port = DatabasePort,
            SecurityGroups = new ISecurityGroup[]
            {
                dbSG
            },
            InstanceType = Amazon.CDK.AWS.EC2.InstanceType.Of(InstanceClass.BURSTABLE3, InstanceSize.SMALL), // Change the instance class
            InstanceIdentifier = $"{Constants.AppName}Database",
            BackupRetention = Duration.Seconds(0)
        });

        // The secret, in Secrets Manager, holds the auto-generated database credentials. Because
        // the secret name will have a random string suffix, we add a deterministic parameter in
        // Systems Manager to contain the actual secret name.
        _ = new StringParameter(this, $"{Constants.AppName}DbSecret", new StringParameterProps
        {
            ParameterName = $"/{Constants.AppName}/dbsecretsname",
            StringValue = this.Database.Secret.SecretName
        });
    }
}