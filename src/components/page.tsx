import * as React from "react";
import { ConnectedAuthComponent } from "./authentication/auth";
import { store } from "..";

export interface IPageComponentDescriptor {
}

export class PageComponent extends React.Component<IPageComponentDescriptor> {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <React.Fragment>
                <ConnectedAuthComponent store={store} />
            </React.Fragment>
        );
    }
}
