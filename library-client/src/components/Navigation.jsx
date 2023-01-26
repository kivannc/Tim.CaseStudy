import React from 'react';
import { Navbar, Nav, NavItem, Container } from 'react-bootstrap';
import { Link, Outlet } from 'react-router-dom';

const Navigation = () => {
  return (
    <div>
      <Navbar bg="dark" variant="dark">
        <Container>
          <Navbar.Brand href="/">Library App</Navbar.Brand>
          <Nav className="me-auto">
            <NavItem>
              <Link to="/" className="nav-link">
                Home
              </Link>
            </NavItem>
            <NavItem>
              <Link to="/reports" className="nav-link">
                Reports
              </Link>
            </NavItem>
          </Nav>
        </Container>
      </Navbar>
      <Outlet />
    </div>
  );
};

export default Navigation;
