

using NP.TCP_Manger;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Channels;

var ip = IPAddress.Parse("10.2.23.1");
var port = 27001;
var client = new TcpClient();

client.Connect(ip, port);

var stream = client.GetStream();
var br = new BinaryReader(stream);
var bw = new BinaryWriter(stream);

Command command = new Command();
string responce = null;
string str = null;

while (true)
{
    Console.WriteLine("write command or help:");
    str = Console.ReadLine().ToUpper();
    if (str == "Help")
    {
        Console.WriteLine();
        Console.WriteLine("Command List:");
        Console.WriteLine(Command.ProccessList);
        Console.WriteLine($"{Command.Run}<process_name");
        Console.WriteLine($"{Command.Kill}<process_name");
        Console.WriteLine( "HELP");
        Console.ReadLine();
        Console.Clear();

    }

    var input=str.Split(' ');
    switch (input[0])
    {
        case Command.ProccessList:
            command = new Command { Text = input[0] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            var processList = JsonSerializer.Deserialize<string[]>(responce);
            foreach(var processName in processList)
            {
                Console.WriteLine($"{processName}");
            }
            break;

        case Command.Run:
            break;
        case Command.Kill:
            break;
        default:
            break;

    }

    Console.WriteLine("press and key to countne");
    Console.ReadLine() ; 
    Console.Clear() ;


}