import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import {
    Collapse,
    Container,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
    NavbarText
} from 'reactstrap';
import '../css/menu.css';



export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
        collapsed: true,
        isModelBuilderSubmenuOpen: false,
        anchorEl: null
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }
 
 

    handleClick = (event) => {
        this.setState({
            isModelBuilderSubmenuOpen: !this.state.isModelBuilderSubmenuOpen,
            anchorEl: event.currentTarget 
        });
    };

    handleClose = () => {
        this.setState({
            isModelBuilderSubmenuOpen: false,
        });
    };

  render () {
      return (
          <div>
              <header style={{ backgroundColor: '#2d333b'}}>
                  <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white  box-shadow mb-3" light>
                      <Container>
                          <NavLink className="text-white" tag={Link} to="/">Home</NavLink>
                          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                              <ul className="navbar-nav flex-grow">
                                  <NavItem>
                                      <NavLink tag={Link} className="text-white" to="/training-data">Training Data</NavLink>
                                  </NavItem>
                                  <NavItem>
                                      <NavLink tag={Link} className="text-white" to="/algorithms/forecasting/ssa/model-builder">Ssa Model Builder</NavLink>
                                  </NavItem>
                                   
                                  <UncontrolledDropdown nav inNavbar >
                                      <DropdownToggle nav caret className="text-white">
                                          Regression
                                        </DropdownToggle>
                                      <DropdownMenu right className="">
                                          <DropdownItem>
                                              <NavLink tag={Link} className="text-white" to="/algorithms/regression/model-builder">Model Builder</NavLink>
                                            </DropdownItem>
                                            <DropdownItem>
                                              <NavLink tag={Link} className="text-white" to="/algorithms/regression/prediction">Prediction</NavLink>
                                            </DropdownItem>
                                      </DropdownMenu>
                                  </UncontrolledDropdown>
                              </ul>
                          </Collapse>
                      </Container>
                  </Navbar>
              </header>
              </div>
    );
  }
}
