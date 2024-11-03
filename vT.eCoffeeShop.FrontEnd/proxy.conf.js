module.exports = {
  "/aspire.api": {
    target: process.env["services__orderservice__http__0"],
    pathRewrite: {
      "^/aspire.api": "",
    },
  },
  "/aspire-admin.api": {
    target: process.env["services__adminservice__http__0"],
    pathRewrite: {
      "^/aspire-admin.api": "",
    },
  },
  /*
  "/api": {
    target: process.env["services__adminservice__http__0"],
    ws: true, // Enable WebSocket proxying
    secure: false, // Set to false for local development
    changeOrigin: true, // Needed for virtual hosted sites
    pathRewrite: {
      "^/api": "/api",
    },
  },*/
};
