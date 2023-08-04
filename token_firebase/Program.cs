using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;


FirebaseApp.Create(new AppOptions()
{
	Credential = GoogleCredential.GetApplicationDefault()
});

var username = "astrid";

string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(username);

var payload = new Message()
{
	Notification = new Notification
	{
		Title = "Test",
		Body = "Isi Test"
	},
	Data = new Dictionary<string, string>()
	{
		{ "Astrid","Test" },
		{ "Body","LagiTest"},
	}
};

payload.Topic = customToken;

// Send Message
await FirebaseMessaging.DefaultInstance.SendAsync(payload);

// Connect db
var database = FirestoreDb.Create("input project id");


// Create collection
string collectionName = "test";
string documentId = "user-id-2";

// Mapping Data
var data = new
{
	Name = "John Doe",
	Age = 30,
	Email = "johndoe@example.com"
};

try
{
	// Save Data
	var documentReference = database.Collection(collectionName).Document(documentId);
	await documentReference.SetAsync(data);

	Console.WriteLine("Success");
}
catch (Exception ex)
{
	Console.WriteLine("Failed: " + ex.Message);
}