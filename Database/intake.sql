--
-- PostgreSQL database dump
--

-- Dumped from database version 9.2.4
-- Dumped by pg_dump version 9.2.4
-- Started on 2013-11-18 16:06:22 CST

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

DROP DATABASE intake;
--
-- TOC entry 2212 (class 1262 OID 42759)
-- Name: intake; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE intake WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'C' LC_CTYPE = 'C';


ALTER DATABASE intake OWNER TO postgres;

\connect intake

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- TOC entry 5 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 2213 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 180 (class 3079 OID 11995)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2215 (class 0 OID 0)
-- Dependencies: 180
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 179 (class 1259 OID 42826)
-- Name: Datum; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Datum" (
    datumid bigint NOT NULL,
    userid bigint NOT NULL,
    "Date" timestamp without time zone NOT NULL,
    description character varying(512),
    latitude double precision,
    longitude double precision,
    accuracy double precision,
    "Value" character varying(512),
    tags character varying(64)[]
);


ALTER TABLE public."Datum" OWNER TO postgres;

--
-- TOC entry 178 (class 1259 OID 42824)
-- Name: Datum_datumid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Datum_datumid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Datum_datumid_seq" OWNER TO postgres;

--
-- TOC entry 2217 (class 0 OID 0)
-- Dependencies: 178
-- Name: Datum_datumid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Datum_datumid_seq" OWNED BY "Datum".datumid;


--
-- TOC entry 177 (class 1259 OID 42786)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "User" (
    userid bigint NOT NULL,
    handle character varying(64) NOT NULL,
    name character varying(512),
    passworddigest character(28) NOT NULL,
    created timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 176 (class 1259 OID 42784)
-- Name: User_userid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "User_userid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."User_userid_seq" OWNER TO postgres;

--
-- TOC entry 2220 (class 0 OID 0)
-- Dependencies: 176
-- Name: User_userid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "User_userid_seq" OWNED BY "User".userid;


--
-- TOC entry 2200 (class 2604 OID 42829)
-- Name: datumid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Datum" ALTER COLUMN datumid SET DEFAULT nextval('"Datum_datumid_seq"'::regclass);


--
-- TOC entry 2198 (class 2604 OID 42789)
-- Name: userid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "User" ALTER COLUMN userid SET DEFAULT nextval('"User_userid_seq"'::regclass);


--
-- TOC entry 2206 (class 2606 OID 42834)
-- Name: Datum_datumid_key; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Datum"
    ADD CONSTRAINT "Datum_datumid_key" UNIQUE (datumid);


--
-- TOC entry 2202 (class 2606 OID 42797)
-- Name: User_handle_key; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "User"
    ADD CONSTRAINT "User_handle_key" UNIQUE (handle);


--
-- TOC entry 2204 (class 2606 OID 42795)
-- Name: User_userid_key; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "User"
    ADD CONSTRAINT "User_userid_key" UNIQUE (userid);


--
-- TOC entry 2207 (class 2606 OID 42835)
-- Name: Datum_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Datum"
    ADD CONSTRAINT "Datum_userid_fkey" FOREIGN KEY (userid) REFERENCES "User"(userid);


--
-- TOC entry 2214 (class 0 OID 0)
-- Dependencies: 5
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- TOC entry 2216 (class 0 OID 0)
-- Dependencies: 179
-- Name: Datum; Type: ACL; Schema: public; Owner: postgres
--

REVOKE ALL ON TABLE "Datum" FROM PUBLIC;
REVOKE ALL ON TABLE "Datum" FROM postgres;
GRANT ALL ON TABLE "Datum" TO postgres;
GRANT ALL ON TABLE "Datum" TO application;


--
-- TOC entry 2218 (class 0 OID 0)
-- Dependencies: 178
-- Name: Datum_datumid_seq; Type: ACL; Schema: public; Owner: postgres
--

REVOKE ALL ON SEQUENCE "Datum_datumid_seq" FROM PUBLIC;
REVOKE ALL ON SEQUENCE "Datum_datumid_seq" FROM postgres;
GRANT ALL ON SEQUENCE "Datum_datumid_seq" TO postgres;
GRANT ALL ON SEQUENCE "Datum_datumid_seq" TO application;


--
-- TOC entry 2219 (class 0 OID 0)
-- Dependencies: 177
-- Name: User; Type: ACL; Schema: public; Owner: postgres
--

REVOKE ALL ON TABLE "User" FROM PUBLIC;
REVOKE ALL ON TABLE "User" FROM postgres;
GRANT ALL ON TABLE "User" TO postgres;
GRANT ALL ON TABLE "User" TO application;


--
-- TOC entry 2221 (class 0 OID 0)
-- Dependencies: 176
-- Name: User_userid_seq; Type: ACL; Schema: public; Owner: postgres
--

REVOKE ALL ON SEQUENCE "User_userid_seq" FROM PUBLIC;
REVOKE ALL ON SEQUENCE "User_userid_seq" FROM postgres;
GRANT ALL ON SEQUENCE "User_userid_seq" TO postgres;
GRANT ALL ON SEQUENCE "User_userid_seq" TO application;


-- Completed on 2013-11-18 16:06:23 CST

--
-- PostgreSQL database dump complete
--

