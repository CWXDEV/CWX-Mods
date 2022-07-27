import { DependencyContainer } from "tsyringe";
import { IPreAkiLoadMod } from "@spt-aki/models/external/IPreAkiLoadMod";
import { ILogger } from "@spt-aki/models/spt/utils/ILogger";

class CWX_Desharpner implements IPreAkiLoadMod
{
    private pkg;

    public preAkiLoad(container: DependencyContainer): void
    { 
        // get the logger from the server container
        const logger = container.resolve<ILogger>("WinstonLogger");
		this.pkg = require("../package.json")
        logger.info(`Loading: ${this.pkg.author}: ${this.pkg.name} - ${this.pkg.version}`);
    }
}

module.exports = { mod: new CWX_Desharpner() }