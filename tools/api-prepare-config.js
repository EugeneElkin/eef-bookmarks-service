"use strict";

const fs = require('fs');
const appRoot = require('app-root-path');

const apiPhysicalPath = appRoot.path + "\\dist_api";
const tmpDir = appRoot.path + "\\.tmp";
const outputConfigFilePath = tmpDir + "\\apihost.config";

fs.readFile("./tools/apihost.config.template", 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }

    const result = data.replace("[PUBLISHED_API_ABSOLUTE_PATH]", apiPhysicalPath);

    
    if (!fs.existsSync(tmpDir)) {
        fs.mkdir(tmpDir, { recursive: true }, (err) => {
            if (err) {
                return console.log(err);
            }
        });
    }

    fs.writeFile(outputConfigFilePath, result, 'utf8', function (err) {
        if (err) {
            return console.log(err);
        }
    });
});

console.log("The location of IISExpress configuration file is [" + outputConfigFilePath + "]");
console.log("The WebAPI host path was set as [" + apiPhysicalPath + "]");