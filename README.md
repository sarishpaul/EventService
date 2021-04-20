# Steps to run the solution
1. Inistall RabbitMQ locally or you can use the available installation
2. Find appsettings.json in the EventService project and provide the credentials for RabbitMQ and SQL Server connection string
3. Either Swagger or Postman can be used to test the solution
4. While testing please remember to remove the Id from the payload (both person and event).


#Questions
After you've finished the solution, we would like you to complete a few questions. Put the answers in the readme of your submission.
1.	If you had more time, what would you change or focus more time on? 
    I haven't focussed much on the naming conventions and unit tests. Also I should have avoided Id's as part of the payload samples.
2.	Which part of the solution consumed the most amount of time? - Creating the infrastructure for EntityFrameswork, RabbitMQ took most of the time
3.	What would you suggest to the clinicians that they may not have thought of in regard to their request? - Though we use micro services, it is possible that one of them is down which will prevent seeing the updates in real time. But the data will not be lost and will sit in the queue
