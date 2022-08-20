import { injectable } from "tsyringe";
import { IConfig } from "../models/IConfig";

@injectable()
export class CWX_ConfigHandler
{
    private config: IConfig;

    constructor()
    {
        this.config = require("../config/config.json");
    }

    public getConfig(): IConfig
    {
        return this.config;
    }
}