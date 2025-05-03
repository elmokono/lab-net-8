aws --endpoint-url=http://localhost:4566 lambda create-function --function-name process-product --runtime dotnet8 --role arn:aws:iam::000000000000:role/lambda-execute-role --handler ProductQueueLambda::ProductQueueLambda.Function::FunctionHandler --zip-file fileb://ProductQueueLambda.zip
aws --endpoint-url=http://localhost:4566 sqs create-queue --queue-name product-creation-queue
aws --endpoint-url=http://localhost:4566 lambda create-event-source-mapping --function-name process-product --event-source-arn arn:aws:sqs:us-east-1:000000000000:product-creation-queue

docker run --name mongodb -p 27017:27017 -d mongodb/mongodb-community-server:latest