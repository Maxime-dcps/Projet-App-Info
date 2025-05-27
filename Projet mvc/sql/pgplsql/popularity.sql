CREATE OR REPLACE FUNCTION popularity(p_listing_id INTEGER)
RETURNS INTEGER AS 
'
DECLARE
	v_count INTEGER;
BEGIN
	SELECT count(*) INTO v_count FROM favorites WHERE listing_id = p_listing_id;
	RETURN v_count;
END;
'
LANGUAGE 'plpgsql';