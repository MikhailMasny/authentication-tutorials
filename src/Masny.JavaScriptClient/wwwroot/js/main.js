var config = {
    authority: "https://localhost:44360/",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:44379/Home/SignIn",
    response_type: "id_token token",
    scope: "openid Masny.IdentityApiOne"
};

var userManger = new Oidc.UserManager(config);

var signIn = function () {
    userManger.signinRedirect();
};

userManger.getUser().then(user => {
    console.log("user:", user);
    if (user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
});

var callApi = function () {
    axios.get("https://localhost:44384/secret")
        .then(res => {
            console.log(res);
        });
} 