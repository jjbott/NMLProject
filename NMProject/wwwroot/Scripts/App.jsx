class Nav extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <Reactstrap.Navbar inverse fixed="top" className="bg-inverse">
                    <Reactstrap.NavbarBrand href="/">NML Project</Reactstrap.NavbarBrand>
                </Reactstrap.Navbar>
            </div>
        );
    }
}

class Property extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "value": this.props.value
        };

        this.handleChange = this.handleChange.bind(this);
    }
    handleChange(e) {
        this.setState({"value": e.target.value });
    }
    render() {
        return (
            <Reactstrap.Input type="select" name="property" value={this.state.value} onChange={this.handleChange}>
                <option value="patent_title">Title</option>
                <option value="patent_abstract">Abstract</option>
                <option value="inventor_first_name">Inventor First Name</option>
                <option value="inventor_last_name">Inventor Last Name</option>
            </Reactstrap.Input>
        );
    }
}

class Operation extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "value": this.props.value
        };
        this.handleChange = this.handleChange.bind(this);
    }
    handleChange(e) {
        this.setState({ "value": e.target.value });
    }
    render() {
        return (
            <Reactstrap.Input name="operation" type="select" value={this.state.value} onChange={this.handleChange}>
                <option value="EQ">==</option>
                <option value="Contains">Contains</option>
            </Reactstrap.Input>
        );
    }
}

class Criteria extends React.Component {
    constructor(props) {
        super(props);
        props.onChange = props.onChange || function () { };
        this.state = {
            "property": this.props.property || "patent_title",
            "operation": this.props.operation || "EQ",
            "value": this.props.value,
        };
        this.handleChange = this.handleChange.bind(this);
        this.notifyChange = this.notifyChange.bind(this);
    }
    componentDidMount() {
        // Make sure new criteria state gets set to whatever the default options are
        this.notifyChange();
    }
    notifyChange() {
        this.props.onChange([this.state.property, this.state.operation, this.state.value]);
    }
    handleChange(e) {
        var callback = () => this.notifyChange();
        switch (e.target.name) {
            case "property": this.setState({ "property": e.target.value }, callback); break;
            case "operation": this.setState({ "operation": e.target.value }, callback); break;
            case "value": this.setState({ "value": e.target.value }, callback); break;
        }
        
    }
    render() {
        return (    
            <div className="p-1 rounded card-outline-secondary">
                <div className="form-inline" onChange={this.handleChange}>
                    <Property value={this.state.property} />
                    <Operation value={this.state.operation}/>
                    <Reactstrap.Input type="text" name="value" value={this.state.value} onChange={this.handleChange} />{' '}
                    {this.props.canRemove &&
                        <Reactstrap.Button onClick={this.props.handleRemove}>✖</Reactstrap.Button>
                    }
                </div>
            </div>
        );
    }
}


class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            criteria: [["patent_title", "EQ", "", 0]],
            nextId: 1
        };
        this.handleAdd = this.handleAdd.bind(this);
        this.handleSearch = this.handleSearch.bind(this);
    }
    handleAdd() {
        this.setState((prevState) => {
            var newCriteria = prevState.criteria;
            newCriteria.push(["", "", "", prevState.nextId]);
            return {
                criteria: newCriteria,
                nextId: ++prevState.nextId
            };
        });
    }
    handleSearch() {
        this.setState({ "queryString": JSON.stringify(this.state.criteria) });
    }
    handleChange(index, newCriteria) {
        this.setState((prevState) => {
            var criterion = prevState.criteria;
            criterion[index][0] = newCriteria[0];
            criterion[index][1] = newCriteria[1];
            criterion[index][2] = newCriteria[2];
            return { "criteria": criterion };
        });
    }
    handleRemove(index) {
        this.setState((prevState) => {
            var criterion = prevState.criteria;
            criterion.splice(index, 1);
            return { "criteria": criterion };
        });
    }
    render() {
        const criterion = this.state.criteria.map((c, i) =>
            <Criteria key={c[3]} property={c[0]} operation={c[1]} value={c[2]} onChange={this.handleChange.bind(this, i)} handleRemove={this.handleRemove.bind(this, i)} canRemove={i != 0}/>
        );
        return (
            <div>
                {criterion}
                <Reactstrap.Button color="primary" onClick={this.handleAdd}>Add Criteria</Reactstrap.Button>
                <Reactstrap.Button color="primary" onClick={this.handleSearch} default className="m-3">Search</Reactstrap.Button>
                {/*<div>Query debugger: {JSON.stringify(this.state.criteria)}</div>*/}
                <SearchResults query={this.state.queryString}/>
            </div>
        );
    }
}

class SearchResult extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isOpen : false 
        };

        this.handleExpand = this.handleExpand.bind(this);
    }

    handleExpand() {
        this.setState((prevState) => {
            return { isOpen: !prevState.isOpen };
        });
    }

    render() {
        var buttonText = this.state.isOpen ? "Hide Abstract" : "Show Abstract";
        return (
            <div>
                <Reactstrap.Card>
                    <h5 className="card-title">
                        {/* Titles are html. Hopefully they're not full of XSS attacks*/}
                        <span dangerouslySetInnerHTML={{ __html: this.props.patent_title }} />
                        {/*{this.props.patent_title} */}
                    </h5>
                    <Reactstrap.CardSubtitle>
                        <Reactstrap.Container>
                            <Reactstrap.Row>
                                <Reactstrap.Col md="6" lg="3" className="text-nowrap"><strong>ID:</strong> {this.props.patent_id}</Reactstrap.Col>
                                <Reactstrap.Col md="6" lg="3" className="text-nowrap"><strong>Date Granted:</strong> {this.props.patent_date}</Reactstrap.Col>
                                <Reactstrap.Col md="6" lg="3" className="text-nowrap"><strong>Inventor:</strong> {this.props.inventor}</Reactstrap.Col>
                                <Reactstrap.Col md="6" lg="3" className="text-nowrap"><Reactstrap.Button onClick={this.handleExpand} className="p-1">{buttonText}</Reactstrap.Button></Reactstrap.Col>
                            </Reactstrap.Row>
                        </Reactstrap.Container>
                    </Reactstrap.CardSubtitle>
                    <Reactstrap.Collapse isOpen={this.state.isOpen}>
                            <Reactstrap.CardBlock>
                                {this.props.patent_abstract}
                            </Reactstrap.CardBlock>
                    </Reactstrap.Collapse>
                </Reactstrap.Card>
                
            </div>
        );
    }
}

class SearchResults extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "results": [],
            "query": this.props.query
        };
        this.fetchResults = this.fetchResults.bind(this);
    }

    fetchResults() {
        if (this.state.query) {
            fetch("/NM/api/PatentViewSearch?queryJson=" + this.state.query)
                .then((response) => {
                    return response.json();
                })
                .then((data) => {
                    this.setState({
                        "results": data || [],
                        hasSearched: true
                    });
                });
        }
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps.query != this.props.query) {
            this.setState({ "query": this.props.query }, this.fetchResults);
        }
    }

    componentDidMount() {
        this.fetchResults();
    }
    
    render() {
        if (this.state.hasSearched) {
            const resultListItems = this.state.results.map((patent) =>
                <SearchResult
                    key={patent.patent_id}
                    patent_id={patent.patent_id}
                    patent_title={patent.patent_title}
                    patent_abstract={patent.patent_abstract}
                    patent_date={patent.patent_date}
                    inventor={((patent.inventor_first_name || "") + ' ' + (patent.inventor_last_name || "")).trim()}
                />
            );
            return (
                <div>
                    <h4>{resultListItems.length} Search Results</h4>
                    <div>{resultListItems}</div>
                </div>
            );
        } else {
            return (
                <div>
                </div>
            );
        }

    }
}

//
ReactDOM.render(
    <Nav />,
    document.getElementById('nav')
);

ReactDOM.render(
    <App/>,
    document.getElementById('content')
);
