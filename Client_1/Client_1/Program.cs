using System.Text;
using System.Net.Sockets;

var client = new TcpClient("127.0.0.1", 9999);

NetworkStream networkStream = client.GetStream();

var writer = new StreamWriter(networkStream);
using var reader = new StreamReader(networkStream, Encoding.UTF8);

var message = "hello server";
Console.WriteLine(message);
byte[] bytes = Encoding.UTF8.GetBytes(message);
networkStream.Write(bytes, 0, bytes.Length);

StreamReader sr = new StreamReader(networkStream);
string response = sr.ReadLine();
Console.WriteLine(response);

networkStream.Close();
client.Close();
Console.ReadKey();
