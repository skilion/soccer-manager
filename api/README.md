# Documentation for SoccerManager

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *http://localhost*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*MarketApi* | [**getMarketList**](Apis/MarketApi.md#getmarketlist) | **GET** /Market | Get the list of players on the market
*PlayerApi* | [**buyPlayer**](Apis/PlayerApi.md#buyplayer) | **POST** /Player/{id}/Buy | Buys a player
*PlayerApi* | [**updatePlayer**](Apis/PlayerApi.md#updateplayer) | **POST** /Player/{id} | Update the details of a player
*PlayerApi* | [**getPlayer**](Apis/PlayerApi.md#getplayer) | **GET** /Player/{id} | Get the details of a player
*PlayerApi* | [**sellPlayer**](Apis/PlayerApi.md#sellplayer) | **POST** /Player/{id}/Sell | Puts a player on the market
*TeamApi* | [**updateTeam**](Apis/TeamApi.md#updateteam) | **PUT** /Team | Updates the details of the team of the current user
*TeamApi* | [**getTeam**](Apis/TeamApi.md#getteam) | **GET** /Team | Get the team of the team of the current user
*UserApi* | [**authenticateUser**](Apis/UserApi.md#authenticateuser) | **POST** /User/Authenticate | Authenticates an user
*UserApi* | [**registerUser**](Apis/UserApi.md#registeruser) | **POST** /User/Register | Creates a new user


<a name="documentation-for-models"></a>
## Documentation for Models

 - [AuthRequest](./Models/AuthRequest.md)
 - [AuthResponse](./Models/AuthResponse.md)
 - [EditPlayerRequest](./Models/EditPlayerRequest.md)
 - [EditTeamRequest](./Models/EditTeamRequest.md)
 - [Player](./Models/Player.md)
 - [PlayerRole](./Models/PlayerRole.md)
 - [RegisterRequest](./Models/RegisterRequest.md)
 - [SellRequest](./Models/SellRequest.md)
 - [Team](./Models/Team.md)
 - [Transfer](./Models/Transfer.md)


<a name="documentation-for-authorization"></a>
## Documentation for Authorization

<a name="Bearer"></a>
### Bearer

- **Type**: JWT

