const tokenKey = 'jwtToken'

let TokenCache = {
    getToken() {
        return localStorage.getItem(tokenKey)
    },
    setToken(newToken) {
        localStorage.setItem(tokenKey, newToken)
    },
    deleteToken() {
        localStorage.removeItem(tokenKey)
    }
}

export default TokenCache