This Blazor WebAssembly application allows users to interact with two different external APIs:

Star Wars API: Users can input a character ID to retrieve information about a Star Wars character, including name, height, mass, hair color, and more. The data is displayed in a card format.

Weather API: Users can search by city name to retrieve current weather conditions.

The application sends requests asynchronously to a backend API, which interacts with the external APIs and returns the results to be displayed in the UI.

### Developer Notes:

To run the OneStream API Coding Assessment, you must run **both** the Blazor WebAssembly client and the API backend projects **simultaneously**. Ensure that the port numbers in each project's `launchSettings.json` are correctly configured for cross-origin communication.

For example:
- **Blazor WASM Client**: Ensure it's running on `https://localhost:5001`.
- **API Backend**: Ensure it runs on a different port, e.g., `https://localhost:5002`.

Verify that both projects are running to allow proper communication between the client and backend services.

![image](https://github.com/user-attachments/assets/22fd222c-97f9-4342-a600-040666f5b509)
![image](https://github.com/user-attachments/assets/3a15bb38-830a-4608-bd92-c5389ed16ee2)
