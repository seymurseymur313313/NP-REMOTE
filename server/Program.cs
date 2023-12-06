using server;
using System.Net;
using System.Text.Json;
using System.Net.Sockets;
using System.Diagnostics;

var ip = IPAddress.Parse("10.2.27.6");
var port = 27001;
var listener=new TcpListener(ip, port);
listener.Start();

while (true) {

    var client = listener.AcceptTcpClient();  
    var stream = client.GetStream();
    var br = new BinaryReader(stream);
    var bw = new BinaryWriter(stream);

    while (true) {

        var input = br.ReadString();
        var command = JsonSerializer.Deserialize<Command>(input);
        if (command == null) continue;

        Console.WriteLine(command.Text);

        switch (command.Text) {
            case Command.ProccessList:
                var processes = Process.GetProcesses();
                var processesNames=JsonSerializer.Serialize(processes.Select(p=>p.ProcessName));
                bw.Write(processesNames);
                break;
            case Command.Run:
                Process.Start(command.Param!);
                break;
            case Command.Kill:
                var killprocesses = Process.GetProcesses();

                foreach (var process in killprocesses) 
                    if (process.ProcessName == command.Param)
                        process.Kill();
                break;
            default:
                break;
        }
    }
}
