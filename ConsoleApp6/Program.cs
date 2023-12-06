using System.Net;
using NP.TCP_Manger;
using System.Text.Json;
using System.Net.Sockets;

var ip = IPAddress.Parse("10.2.27.6");
var port = 27001;
var client = new TcpClient();
client.Connect(ip, port);
Console.WriteLine();

var stream = client.GetStream();
var br = new BinaryReader(stream);
var bw = new BinaryWriter(stream);

Command command = new Command();
string? responce = null;
string? str = null;

while (true) {

    Console.WriteLine("write command or help:");
    str = Console.ReadLine(); // RUN Calc

    if (str == "Help") {
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
    input[0] = input[0].ToUpper();

    switch (input[0]) {
        case Command.ProccessList:
            command = new Command { Text = input[0] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            var processList = JsonSerializer.Deserialize<string[]>(responce);
            foreach(var processName in processList)
                Console.WriteLine($"{processName}");
            break;

        case Command.Run:
            command = new Command { Text = input[0], Param = input[1] };
            bw.Write(JsonSerializer.Serialize(command));
            break;
        case Command.Kill:
            command = new Command { Text = input[0], Param = input[1] };
            bw.Write(JsonSerializer.Serialize(command));
            break;
        default:
            break;

    }

    Console.WriteLine("press and key to countne");
    Console.ReadLine() ; 
    Console.Clear() ;
}