# IoT Data Management System

Documentation for software in Bachelor Project.

**Students:**

- Anes Skrijelj (anskr@itu.dk)
- Mikkel Møller Jensen (momj@itu.dk)
- Rune Engelbrecht Henriksen (rhen@itu.dk)



## About the system

### Architecture



### Design Principles





## Running the system

The system is developed in the lanugage C# using .NET 6. By this, the system can be run on any device as long as this is installed. 

#### Dependencies

- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [NetMQ](https://netmq.readthedocs.io/en/latest/) (port of [ZeroMQ](https://zeromq.org/))
- [Quartz](https://www.quartz-scheduler.net/)
- [Serilog](https://serilog.net/)

#### Restrictions

A major component, the NodeEngine, is made to work universally with any devices with .NET 6 installed. However, queries, with sensors as goal, will only work if the NodeEngine is running on a Linux-machine. The internal sensors are measured with commands only available on Linux.

If the NodeEngine can't be started on a Linux-machine, it is possible to send a query with a 'Test'-sensor. More info below.
