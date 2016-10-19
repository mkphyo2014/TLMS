using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;
using ServiceStack;
using ServiceStack.OrmLite;

namespace TLMS.Infrastructure.Init
{
    public static class NodaTimeSqlConverters
    {
        public static void RegisterNodaTimeSqlConverters()
        {
            SqlServerDialect.Provider.RegisterConverter<Instant>(new InstantSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<Instant?>(new NullableInstantSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<LocalDate>(new LocalDateSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<LocalDate?>(new NullableLocalDateSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<LocalDateTime>(new LocalDateTimeSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<LocalDateTime?>(new NullableLocalDateTimeSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<ZonedDateTime>(new ZonedDateTimeSqlServerConverter());
            SqlServerDialect.Provider.RegisterConverter<ZonedDateTime?>(new NullableZonedDateTimeSqlServerConverter());
        }
    }
    public class InstantSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "DATETIME2";

        public override DbType DbType => DbType.DateTime2;

        public override object ToDbValue(Type fieldType, object value)
        {
            var instantValue = (Instant)value;
            var dateTimeUnspecified = instantValue.ToDateTimeUtc();
            return dateTimeUnspecified;
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var datetimeValue = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
            return Instant.FromDateTimeUtc(datetimeValue);
        }
    }

    public class NullableInstantSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "DATETIME2";

        public override DbType DbType => DbType.DateTime2;

        public override void InitDbParam(IDbDataParameter p, Type fieldType)
        {
            var sqlParam = (SqlParameter)p;
            sqlParam.SqlDbType = SqlDbType.Udt;
            sqlParam.IsNullable = fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Nullable<>);
            sqlParam.UdtTypeName = ColumnDefinition;
        }

        public override object ToDbValue(Type fieldType, object value)
        {
            var instantValue = (Instant?)value;

            var dateTimeUnspecified = instantValue?.ToDateTimeUtc();
            return dateTimeUnspecified;
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var dateTime = (DateTime?)value;
            if (!dateTime.HasValue) return null;
            var datetimeValue = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            return Instant.FromDateTimeUtc(datetimeValue);
        }
    }


    public class LocalDateSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "DATE";
        public override DbType DbType => DbType.Date;

        public override object ToDbValue(Type fieldType, object value)
        {
            var localDate = (LocalDate)value;
            return new DateTime(localDate.Year, localDate.Month, localDate.Day);
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var dateTime = (DateTime)value;
            return new LocalDate(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }

    public class NullableLocalDateSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "DATE";

        public override DbType DbType => DbType.Date;

        public override void InitDbParam(IDbDataParameter p, Type fieldType)
        {
            var sqlParam = (SqlParameter)p;
            sqlParam.SqlDbType = SqlDbType.Udt;
            sqlParam.IsNullable = fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Nullable<>);
            sqlParam.UdtTypeName = ColumnDefinition;
        }

        public override object ToDbValue(Type fieldType, object value)
        {
            var localDate = (LocalDate?)value;
            if (!localDate.HasValue) return null;
            return new DateTime(localDate.Value.Year, localDate.Value.Month, localDate.Value.Day);
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var dateTime = (DateTime?)value;
            if (!dateTime.HasValue) return null;
            return new LocalDate(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
        }
    }


    public class LocalDateTimeSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "DATETIME2";

        public override DbType DbType => DbType.DateTime2;

        public override object ToDbValue(Type fieldType, object value)
        {
            var localDateTime = (LocalDateTime)value;
            var dateTimeUnspecified = localDateTime.ToDateTimeUnspecified();
            return dateTimeUnspecified;
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var datetimeValue = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
            return Instant.FromDateTimeUtc(datetimeValue);
        }
    }

    public class NullableLocalDateTimeSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "DATETIME2";

        public override DbType DbType => DbType.DateTime2;


        public override void InitDbParam(IDbDataParameter p, Type fieldType)
        {
            var sqlParam = (SqlParameter)p;
            sqlParam.SqlDbType = SqlDbType.Udt;
            sqlParam.IsNullable = fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Nullable<>);
            sqlParam.UdtTypeName = ColumnDefinition;
        }

        public override object ToDbValue(Type fieldType, object value)
        {
            var localDateTimeValue = (LocalDateTime?)value;

            var dateTimeUnspecified = localDateTimeValue?.ToDateTimeUnspecified();
            return dateTimeUnspecified;
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var dateTime = (DateTime?)value;
            if (!dateTime.HasValue) return null;
            return new LocalDateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, dateTime.Value.Hour, dateTime.Value.Minute, dateTime.Value.Second);
        }
    }


    public class ZonedDateTimeSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "VARCHAR(100)";
        public override DbType DbType => DbType.String;

        public override object ToDbValue(Type fieldType, object value)
        {
            var zonedDateTime = (ZonedDateTime)value;
            return zonedDateTime.ToJsv();
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var zonedDateTimeString = (string)value;
            return zonedDateTimeString.FromJsv<ZonedDateTime>();
        }
    }

    public class NullableZonedDateTimeSqlServerConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "VARCHAR(100)";
        public override DbType DbType => DbType.String;
        public override void InitDbParam(IDbDataParameter p, Type fieldType)
        {
            var sqlParam = (SqlParameter)p;
            sqlParam.SqlDbType = SqlDbType.Udt;
            sqlParam.IsNullable = fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Nullable<>);
            sqlParam.UdtTypeName = ColumnDefinition;
        }

        public override object ToDbValue(Type fieldType, object value)
        {
            var zonedDateTime = value as ZonedDateTime?;
            return zonedDateTime?.ToJsv();
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var zonedDateTimeString = (string)value;
            if (string.IsNullOrEmpty(zonedDateTimeString) || string.IsNullOrWhiteSpace(zonedDateTimeString)) return null;
            return zonedDateTimeString.FromJsv<ZonedDateTime>();
        }
    }


}
