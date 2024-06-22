This repository provides an explanation of gRPC: What is it? Why use gRPC? How is it different from REST? + a gRPC vs. REST comparison PROJECT.

### ❓ What is gRPC?
gRPC is an architecture and an open-source API system based on the RPC (Remote Procedure Call) model. Although RPC is a broad concept, gRPC is a specific implementation.

In RPC, client-server communications work as if the client API requests were a local operation or as if the request were internal server code.
gRPC is a system that implements traditional RPC with some optimizations. For example, gRPC uses _Protocol Buffers_ and _HTTP 2_ for data transmission.

### ❔ Why use gRPC?

gRPC was designed specifically for the development of high-performance APIs. It is best suited to internal systems that require real-time streaming and large data loads. A gRPC API may be a better option for some cases such as:

- Secure, high-performance systems
- High data loads
- Real-time or streaming applications

**Some features that make gRPC more performant such as:**

📑 **Use Protocol Buffers** as default data serialization format - his format is binary and more compact than text formats such as _JSON_, and results in lower bandwidth consumption and less overhead during data transmission
   - because it is a binary format and has an explicitly defined data schema (using a .proto file), data serialization and deserialization tend to be faster when compared to text formats such as JSON, where the data structure is less rigorous, which generates less overhead during data serialization and deserialization
     
💡 **Use HTTP/2** as default transport protocol

➿ **Bidirectional Streaming** - which means that both the client and the server can send a continuous sequence of messages and can be useful in cases involving real time communications or transferring large amounts of data.

📉 **Lower latency** - the efficiency of HTTP/2 combined with lower data overhead and bidirectional streaming capabilities means that gRPC generally has lower latencies compared to _REST_, for example, especially in environments where fast and efficient communication is crucial.

### gRPC x REST - Summary of differences

|                     | gRPC API                                                                                          | REST API                                                                                            |
|---------------------|----------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|
| WHAT IS IT?         | A system for creating and using APIs based on the client-server communication model of RPC                                             | A set of rules that defines the structured data exchange between a client and a server             |
| DESIGN APPROACH     | Service oriented design. The client requests the server to execute a service or function that may or may not affect server resources.                          | Entity oriented design. The client requests the server to create, share, or modify resources.        |
| IMPLEMENTATION      | Requires gRPC software on client and server sides to operate                                  | Can be implemented on client and server sides in a wide variety of formats without the need for common software                                              |
| DATA ACCESS        | Service calls or functions                                                                          | Multiple endpoints in the form of URLs to define resources                                           |
| RETURNED DATA       | In the fixed return type of the service, as defined in the **Protocol Buffer** file                   | In a fixed structure (usually JSON), defined by the server                                           |
| CLIENTE-SERVER COUPLING | Tightly coupled. Client and server need the same protocol buffer file defining the data format | Loosely coupled. The client and server are not aware of each other's internal details             |
| BIDIRECTIONAL STREAMING | Present                                                                                          | Not present                                                                                         |
| BEST SUITED FOR     | High-performance microservices architectures or those dealing with large amounts of data          | Simple data sources where resources are clearly defined                                               |


## ✨ HANDS ON!

#### 🏃 Installing and Running

1. Clone this repo `https://github.com/kmlyteixeira/grpc-project` 
2. Enter in the **node-client** folder
   1.  Run `npm install` to install the dependencies
   2.  Run `npm start` or `npx ts-node server.ts` to start
   3.  The console should display: *Server is running at `http://localhost:{port}`*
3. Enter in the **grpc-api** folder
   1. Run `dotnet build` and `dotner run` to compile and start the server
   2. The console should display: *Now listening on: `http://localhost:{port}`*
4. Enter in the **rest-api** folder
   1. Run `dotnet build` and `dotner run` to compile and start the server
   2. The console should display: *Now listening on: `http://localhost:{port}`*

#### 🛠️ In this project, you'll find:

**`grpc-api` folder** - a gRPC API build w/ .NET 8

**`rest-api` folder** - a REST API build w/ .NET 8

**`node-client` folder** - a client build w/ NodeJS + Typescript 

For this performance test, both APIs (*REST* and *gRPC*) were developed to return the same data structure, an object with sequential numeric keys, each mapping to a string of random content. Some like this:

```json
{ 
    "1": "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 
    "2": "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    "...",
    "1000": "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
}

```

On the **client side**, two endpoints have been added: `/grpc` and `/rest`

## gRPC x REST Performance Comparision

Once the APIs and the client have been created and properly started, we can run a load test to compare the performance of the services. To do this, we can use the *Apache Benchmarking Tool (ab)* to collect simple metrics. Like this:

#### 1️⃣ REST ENDPOINT

```cmd
ab  -n  1000  -c  100  localhost:{port}/rest
     ↓         ↓
  requests  concurrency
```
#### Result:

```cmd
Finished 1000 requests

Document Path:          /rest
Document Length:        207071 bytes

Concurrency Level:      100
Time taken for tests:   17.951 seconds
Complete requests:      1000
Total transferred:      210564070 bytes
HTML transferred:       210350070 bytes
Requests per second:    55.71 [#/sec] (mean)
Time per request:       1795.094 [ms] (mean)
Time per request:       17.951 [ms] (mean, across all concurrent requests)
Transfer rate:          11455.05 [Kbytes/sec] received

Percentage of the requests served within a certain time (ms)
  50%   1278
  66%   2584
  75%   2657
  80%   2706
  90%   3002
  95%   3189
  98%   3250
  99%   3251
 100%   3453 (longest request)
```

#### 2️⃣ gRPC ENDPOINT

```cmd
ab -n 1000 -c 100 localhost:{port}/grpc
```
#### Result:

```cmd
Finished 1000 requests

Document Path:          /grpc
Document Length:        211518 bytes

Concurrency Level:      100
Time taken for tests:   9.801 seconds
Complete requests:      1000
Total transferred:      214637097 bytes
HTML transferred:       214423097 bytes
Requests per second:    102.03 [#/sec] (mean)
Time per request:       980.064 [ms] (mean)
Time per request:       9.801 [ms] (mean, across all concurrent requests)
Transfer rate:          21387.01 [Kbytes/sec] received

Percentage of the requests served within a certain time (ms)
  50%    925
  66%   1007
  75%   1057
  80%   1106
  90%   1250
  95%   1504
  98%   1744
  99%   1820
 100%   1987 (longest request)
```

## Conclusion

### 🕐 Average Response Time per Request
*Considering all concurrent requests*

|         REST         |         gRPC         |
|----------------------|----------------------|
|     1795.094 ms      |      980.064 ms      |
|     (17.951 s)       |      (9.801 s)       |

gRPC was significantly faster than REST, with an average response time per request almost twice as fast.

### 🕥 Requests per second

|         REST         |         gRPC         |
|----------------------|----------------------|
|     55.71 req/s      |      102.03 req/s    |

gRPC can handle almost twice as many requests per second as REST.

### 🔄 Transfer rate

|         REST         |         gRPC         |
|----------------------|----------------------|
|  11455.05 Kbytes/s   |   21387.01 Kbytes/s  |

gRPC has a transfer rate almost twice that of REST, indicating better efficiency in data transfer.

### 🔌 Connection and Processing Time

|         | Connection (ms) | Processing Time (ms) |
|---------|--------------|--------------------|
|   REST  |    0-7 (1)   |   58-3453 (1776)   |
|   gRPC  |    0-4 (0)   |   47-1987 (960)    |

Both protocols have low connection times, but gRPC has a significantly lower processing time.

### 💣 Distribution of Request Times

| %         | REST (ms) | gRPC (ms) |
|-----------|-----------|-----------|
|    50%    |   1278    |   925     |
|    95%    |   3189    |   1504    |
|   100%    |   3453    |   1987    |

gRPC offers less variability in response times, with all requests being processed more quickly compared to REST.

### Summary of Conclusions
**Overall Performance:** 
gRPC outperforms REST in all measurable aspects of performance (response time, requests per second, and throughput).

**Consistency:** gRPC shows less variability in response time.

**Efficiency:** gRPC is more efficient in its use of network resources.

## 📚 Learn More
- [Introduction to gRPC](https://grpc.io/docs/what-is-grpc/introduction/)
- [Overview for gRPC on .NET](https://learn.microsoft.com/en-us/aspnet/core/grpc/?view=aspnetcore-8.0)
- [The difference between gRPC and REST](https://aws.amazon.com/pt/compare/the-difference-between-grpc-and-rest/)
- [gRPC vs REST: Performance Comparison](https://www.youtube.com/watch?v=_5W5gtNK4Ow)
