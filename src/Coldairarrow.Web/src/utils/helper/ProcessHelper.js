const productKey = 'production'
const ProcessHelper = {
    isProduction() {
        return process.env.NODE_ENV == productKey
    },
    isPreview() {
        return process.env.VUE_APP_PREVIEW === 'true'
    }
}
export default ProcessHelper