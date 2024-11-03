const { env } = require('process');
const { NodeSDK } = require('@opentelemetry/sdk-node');
const { OTLPTraceExporter } = require('@opentelemetry/exporter-trace-otlp-grpc');
const { OTLPLogExporter } = require('@opentelemetry/exporter-logs-otlp-grpc');
const { SimpleLogRecordProcessor } = require('@opentelemetry/sdk-logs');
const { HttpInstrumentation } = require('@opentelemetry/instrumentation-http');
const { ExpressInstrumentation } = require('@opentelemetry/instrumentation-express');
const { RedisInstrumentation } = require('@opentelemetry/instrumentation-redis-4');
const { credentials } = require('@grpc/grpc-js');
const { diag, DiagConsoleLogger, DiagLogLevel } = require('@opentelemetry/api');

const environment = env['NODE_ENV'] || 'development';
diag.setLogger(new DiagConsoleLogger(), environment === 'development' ? DiagLogLevel.DEBUG : DiagLogLevel.WARN);

const otlpServer = env['OTEL_EXPORTER_OTLP_ENDPOINT'] || 'http://localhost:19075';

if (otlpServer) {
  console.log(`OTLP endpoint: ${otlpServer}`);

  const isHttps = otlpServer.startsWith('https://');
  const collectorOptions = {
    url: otlpServer,
    credentials: isHttps ? credentials.createSsl() : credentials.createInsecure()
  };

  const traceExporter = new OTLPTraceExporter(collectorOptions);
  const logExporter = new OTLPLogExporter(collectorOptions);

  const sdk = new NodeSDK({
    traceExporter,
    logRecordProcessors: [new SimpleLogRecordProcessor(logExporter)],
    instrumentations: [
      new HttpInstrumentation(),
      new ExpressInstrumentation(),
      new RedisInstrumentation()
    ],
  });

  // Start the SDK and log any errors
  sdk.start()

    /*.then(() => {
    console.log('SDK started successfully');
  }).catch(err => {
    console.error('Error starting SDK:', err);
  });*/
} else {
  console.error('OTLP endpoint is not defined. Please set the OTEL_EXPORTER_OTLP_ENDPOINT environment variable.');
}
