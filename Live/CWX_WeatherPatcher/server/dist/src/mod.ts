import { DependencyContainer } from "tsyringe";
import { IMod } from "@spt-aki/models/external/mod";
import { ILogger } from "@spt-aki/models/spt/utils/ILogger";

class CWX_MasterKey implements IMod
{
    private pkg;

    public load(container: DependencyContainer): void
    { 
        // get the logger from the server container
        const logger = container.resolve<ILogger>("WinstonLogger");
		this.pkg = require("../package.json")
        logger.info(`Loading: ${this.pkg.author}: ${this.pkg.name} - ${this.pkg.version}`);
    }

    public delayedLoad(container: DependencyContainer): void
    { return }
}

module.exports = { mod: new CWX_MasterKey() }