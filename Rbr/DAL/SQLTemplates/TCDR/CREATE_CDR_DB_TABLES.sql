USE CdrDb_$(DB_NAME_SUFFIX)
GO


CREATE TABLE CDR (
       date_logged          datetime NOT NULL,
       timok_date           int NOT NULL,
       start                datetime NOT NULL,
       duration             smallint NOT NULL,
       ccode                int NOT NULL,
       local_number         varchar(18) NOT NULL,
       carrier_route_id     int NOT NULL,
       price                decimal(9,5) NOT NULL,
       cost                 decimal(9,5) NOT NULL,
       orig_IP_address      int NOT NULL,
       orig_end_point_id    smallint NOT NULL,
       term_end_point_id    smallint NOT NULL,
       customer_acct_id     smallint NOT NULL,
       disconnect_cause     smallint NOT NULL,
       disconnect_source    tinyint NOT NULL,
       rbr_result           smallint NOT NULL,
       prefix_in            varchar(10) NOT NULL,
       prefix_out           varchar(10) NOT NULL,
       DNIS                 bigint NOT NULL,
       ANI                  bigint NOT NULL,
       serial_number        bigint NOT NULL,
       end_user_price       decimal(6,2) NOT NULL,
       used_bonus_minutes   smallint NOT NULL,
       node_id              smallint NOT NULL,
       customer_route_id    int NOT NULL,
       mapped_disconnect_cause smallint NOT NULL,
       carrier_acct_id      smallint NOT NULL,
       customer_duration    smallint NOT NULL,
       retail_acct_id       int NOT NULL,
       reseller_price       decimal(9,5) NOT NULL,
       carrier_duration     smallint NOT NULL,
       retail_duration      smallint NOT NULL,
       info_digits          tinyint NOT NULL,
       id                   char(32) NULL
)
go

CREATE INDEX XIECDR_Identity ON CDR
(
       id
)
go

CREATE INDEX XIECDR_TimokDate_RetailAcct ON CDR
(
       timok_date,
       retail_acct_id
)
go

CREATE INDEX XIECDR_TimokDate_OEP ON CDR
(
       timok_date,
       orig_end_point_id
)
go

CREATE INDEX XIECDR_TimokDate_TEP ON CDR
(
       timok_date,
       term_end_point_id
)
go

CREATE INDEX XIECDR_TimokDate_customer ON CDR
(
       timok_date,
       customer_acct_id
)
go

CREATE INDEX XIECDR_Date_Logged ON CDR
(
       date_logged
)
go

CREATE INDEX XIECDR_TimokDate_carrier ON CDR
(
       timok_date,
       carrier_acct_id
)
go


CREATE TABLE CDRIdentity (
       id                   char(32) NOT NULL
)
go


ALTER TABLE CDRIdentity
       ADD CONSTRAINT XPKCDRIdentity PRIMARY KEY NONCLUSTERED (id)
go

ALTER TABLE CDR
       ADD CONSTRAINT R_1
              FOREIGN KEY (id)
                             REFERENCES CDRIdentity
go
