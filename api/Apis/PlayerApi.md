# PlayerApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**buyPlayer**](PlayerApi.md#buyPlayer) | **POST** /Player/{id}/Buy | Buys a player
[**updatePlayer**](PlayerApi.md#updatePlayer) | **PUT** /Player/{id} | Update the details of a player
[**getPlayer**](PlayerApi.md#getPlayer) | **GET** /Player/{id} | Get the details of a player
[**sellPlayer**](PlayerApi.md#sellPlayer) | **POST** /Player/{id}/Sell | Puts a player on the market


<a name="buyPlayer"></a>
# **buyPlayer**
> buyPlayer(id)

Buys a player

    The player must be on the market and the user's team must have enough money.  If the user owns the player, the player is simply removed from the market.

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **Integer**|  | [default to null]

### Return type

null (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

<a name="updatePlayer"></a>
# **updatePlayer**
> Player updatePlayer(id, updatePlayerRequest)

Update the details of a player

    The player must belong to the user

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **Integer**|  | [default to null]
 **updatePlayerRequest** | [**updatePlayerRequest**](../Models/updatePlayerRequest.md)|  | [optional]

### Return type

[**Player**](../Models/Player.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json

<a name="getPlayer"></a>
# **getPlayer**
> Player getPlayer(id)

Get the details of a player.

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **Integer**|  | [default to null]

### Return type

[**Player**](../Models/Player.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

<a name="sellPlayer"></a>
# **sellPlayer**
> sellPlayer(id, SellRequest)

Puts a player on the market

    The player must belong to the user's team

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **Integer**|  | [default to null]
 **SellRequest** | [**SellRequest**](../Models/SellRequest.md)|  | [optional]

### Return type

null (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: Not defined

