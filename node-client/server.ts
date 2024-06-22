import express from 'express';
import grpcClient from './services/grpcClient';
import restClient from './services/restClient';

const app = express();
const port = 3000;

app.get('/grpc', (req, res) => {
  grpcClient.GetLargeText({ }, (error: any, response: any) => {
    if (error) {
      console.error('Error calling gRPC API:', error);
      res.status(500).send('Error calling gRPC API');
    } else {
      res.json(response);
    }
  });
});

app.get('/rest', async (req, res) => {
  try {
    const response = await restClient.get('/api/notes/large-text');
    res.json(response.data);
  } catch (error) {
    console.error('Error calling REST API:', error);
    res.status(500).send('Error calling REST API');
  }
});

app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
  console.log(`gRPC Service at http://localhost:${port}/grpc`);
  console.log(`REST Service at http://localhost:${port}/rest`);
});
