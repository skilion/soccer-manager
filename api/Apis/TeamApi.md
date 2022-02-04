# TeamApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**updateTeam**](TeamApi.md#updateTeam) | **PUT** /Team | Updates the details of the team of the current user
[**getTeam**](TeamApi.md#getTeam) | **GET** /Team | Get the team of the current user


<a name="updateTeam"></a>
# **updateTeam**
> Team updateTeam(updateTeamRequest)

Updates the details of the team of the current user

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **updateTeamRequest** | [**updateTeamRequest**](../Models/updateTeamRequest.md)|  | [optional]

### Return type

[**Team**](../Models/Team.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json

<a name="getTeam"></a>
# **getTeam**
> Team getTeam()

Get the team of the current user

### Parameters
This endpoint does not need any parameter.

### Return type

[**Team**](../Models/Team.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

