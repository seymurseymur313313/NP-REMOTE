﻿
using server;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

var ip = IPAddress.Loopback;
var port = 27001;
var listener=new TcpListener(ip, port);
listener.Start();



while (true)
{
    var client=listener.AcceptTcpClient();  
    var stream=client.GetStream();
    var br = new BinaryReader(stream);
    var bw = new BinaryWriter(stream);
    while (true)
    {
        var input=br.ReadString();
        var command=JsonSerializer.Deserialize< Command>(input);

        if (command == null) continue;
        Console.WriteLine(command.Text);
        switch (command.Text)
        {
            case Command.ProccessList:
                var processes = Process.GetProcesses();
                var processesNames=JsonSerializer.Serialize(processes.Select(p=>p.ProcessName));
                bw.Write(processesNames);
                break;
            case Command.Run:
                break;
            case Command.Kill:
                break;
            default:
                break;



        }

    }

}
