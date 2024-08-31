# Sitemate-Full-Stack-Engineer---Challenge-Q3-24
This repository highlights my technical expertise and problem-solving skills, demonstrating my suitability for the Full Stack Engineer role at Sitemate. It features a straightforward yet impactful project that showcases my capabilities and readiness to contribute to the Sitemate team.
# Overview
In this project, issue management is facilitated through a REST API server and client. The server processes and transmits predefined JSON objects that represent issues, each characterized by three attributes: id, title, and description. The client-side application interacts with the server to execute CRUD operations—Create, Read, Update, and Delete—on these issues.
# Technologies Used
* **NET Framework**: For building the REST API server.
* **MySQL**: For data storage and management.
* **Knockout.js**: For data binding and AJAX calls in the client-side application.
* **Tailwind CSS**: For styling the client-side application to create an attractive UI.
# Code Organization
### REST API Server
The server-side code is organized as follows:
* **Controllers**: Contains logic for handling API requests and responses.
* **Models**: Defines the data models and interacts with the database.
* **Repository**: Implements the generic repository pattern for data access.
* **Startup.cs**: Configures services and application settings.
### REST API Client
The client-side application interacts with the REST API server through the following components:
- **ApiClient:** Manages HTTP requests to the server, providing methods to interact with the `SitemateController` endpoints. This service encapsulates the logic for sending requests and receiving responses from the API.
#### Main Client Functionality
- **Create Issue:** Sends a POST request to the `/Sitemate/Add` endpoint to create a new issue.
- **Read Issues:** Sends a GET request to the `/Sitemate/GetAll` endpoint to retrieve a list of all issues. Sends a GET request to `/Sitemate/GetById/{id}` to retrieve a specific issue by its ID.
- **Update Issue:** Sends a PUT request to the `/Sitemate/Update/{id}` endpoint to update an existing issue.
- **Delete Issue:** Sends a DELETE request to the `/Sitemate/Delete/{id}` endpoint to remove an issue.
# Instructions for Running the System

### Clone the Repository

To clone the repository to your local machine:

```bash
git clone https://github.com/Vicky642/Sitemate-Full-Stack-Engineer---Challenge-Q3-24
