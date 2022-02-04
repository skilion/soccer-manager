# UserApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**authenticateUser**](UserApi.md#authenticateUser) | **POST** /User/Authenticate | Authenticates an user
[**registerUser**](UserApi.md#registerUser) | **POST** /User/Register | Creates a new user


<a name="authenticateUser"></a>
# **authenticateUser**
> AuthResponse authenticateUser(AuthRequest)

Authenticates an user

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **AuthRequest** | [**AuthRequest**](../Models/AuthRequest.md)|  | [optional]

### Return type

[**AuthResponse**](../Models/AuthResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json

<a name="registerUser"></a>
# **registerUser**
> registerUser(RegisterRequest)

Creates a new user

    The user will have a random team generated

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **RegisterRequest** | [**RegisterRequest**](../Models/RegisterRequest.md)|  | [optional]

### Return type

null (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: Not defined

